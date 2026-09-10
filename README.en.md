# CertGerador

> Leia em [Português](README.md).

Desktop application built with **.NET / C# (Windows Forms)** that generates certificates in batch from a Word template and a CSV spreadsheet, automatically converting each document to PDF.

Personal project, rewritten from scratch as an exercise in architecture and best practices — with no dependency on any specific organization: any CSV and any `.docx` template will work, as long as the CSV column names match the template's text placeholders.

![Main screen](screenshots/formulario-vazio.webp)

## Features

- Reads data from a **CSV** file (semicolon-delimited), with any set of columns.
- Automatically fills a **`.docx` template**, replacing text placeholders (e.g. `#name`, `#id`) with each CSV row's values.
- Automatically converts every generated document to **PDF**.
- Lets the user pick, via a combo box, **which CSV column** should name each output file.
- **Asynchronous and cancellable** batch processing, with a progress bar.

## Screenshots

| Setup | Processing | Result |
|---|---|---|
| ![Empty form](screenshots/tela-principal.webp) | ![Process complete](screenshots/processo-concluido.webp) | ![Output folder](screenshots/pasta-saida.webp) |

Example certificate generated from the sample template and data:

![Generated certificate](screenshots/certificado-gerado.webp)

## How to use

1. Select the **CSV file** with the data (one row per certificate).
2. Select the **`.docx` template**, with text placeholders matching the CSV column names (e.g. column `#nome` → placeholder `#nome` in the document).
3. Select the **destination folder** where the PDFs will be saved.
4. Choose, from the combo box, **which column** will be used to name each generated file.
5. Click **Gerar Certificados** (Generate Certificates).

## Sample data

The [`SampleData/`](CertGerador/SampleData) folder contains a template (`template_certificado.docx`) and a CSV (`dados_exemplo.csv`) with **fictitious** data, ready to test the application without needing any real data. The CSV column names already match the template's placeholders.

## Architecture

The project separates responsibilities via interfaces, while still keeping a single WinForms project (a deliberate choice — see below):

```
CertGerador/
├── Interfaces/     # Service contracts (ICertificateDataSource, ICertificateDocumentBuilder, IDocumentConverter)
├── Models/         # DTOs (CertificateRecord, BatchSettings, CertificateResult)
├── Services/       # Concrete implementations (CsvHelper, OpenXML SDK, FreeSpire.Doc)
├── SampleData/     # Sample template and CSV, for testing
└── Form1.cs        # UI — delegates all logic to the services through their interfaces
```

- **CSV parsing**: [CsvHelper](https://joshclose.github.io/CsvHelper/).
- **`.docx` generation**: [Open XML SDK](https://github.com/dotnet/Open-XML-SDK) (Microsoft's official library, with no licensing restrictions).
- **PDF conversion**: [FreeSpire.Doc](https://www.e-iceblue.com/Introduce/free-doc-component.html).
- **Concurrency**: `async`/`await` with `Task.Run`, `IProgress<T>` for progress reporting, and `CancellationToken` for cancellation — replacing the `BackgroundWorker` (a pre-`async` pattern) used in the first version of this project.

### Why a single WinForms project instead of multiple projects (Core/Infrastructure/UI)?

This was a conscious decision. A layered architecture with separate projects (and automated tests) would bring real testability and reuse benefits, but for a portfolio project that won't be maintained in production or run in CI, the gain didn't justify the extra friction. Instead, separation of concerns was kept **through folders and interfaces** within the same project — enough to demonstrate the design decisions without the overhead of multiple assemblies.

## Running the project

Prerequisites: .NET 10 SDK, Windows (WinForms doesn't run on other platforms).

```bash
git clone https://github.com/TheuZN/certGerador.git
cd certGerador
dotnet build
dotnet run --project CertGerador
```

## About this project

This is a rewrite of a project originally developed during an internship, now generalized and fully decoupled from the original organization: no real data, no branding, and no organization-specific integration remains in the code — the generator works with any template and any CSV.

## License

Distributed under the MIT License. See [LICENSE](LICENSE) for more information.
