using CertGerador.Interfaces;
using CertGerador.Models;
using CertGerador.Services;

namespace CertGerador
{
    public partial class Form1 : Form
    {
        private readonly ICertificateDataSource _dataSource = new CsvCertificateDataSource();
        private readonly ICertificateDocumentBuilder _documentBuilder = new DocxCertificateBuilder();
        private readonly IDocumentConverter _converter = new SpireDocumentConverter();

        private CancellationTokenSource? _cts;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCsv_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "Arquivo csv|*.csv" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            listBoxCSV.Items.Clear();
            listBoxCSV.Items.Add(ofd.FileName);

            try
            {
                var records = _dataSource.Read(ofd.FileName);
                var columns = records.Count > 0 ? records[0].Fields.Keys : Enumerable.Empty<string>();

                comboFileNameField.Items.Clear();
                comboFileNameField.Items.AddRange(columns.ToArray());
                if (comboFileNameField.Items.Count > 0)
                    comboFileNameField.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível ler o CSV: {ex.Message}");
            }
        }

        private void btnDocx_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "Arquivo docx|*.docx" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            listBoxDOCX.Items.Clear();
            listBoxDOCX.Items.Add(ofd.FileName);
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            using var ofd = new FolderBrowserDialog { Description = "Selecione uma pasta" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            listBoxFOLDER.Items.Clear();
            listBoxFOLDER.Items.Add(ofd.SelectedPath);
        }

        private async void btnGerar_Click(object sender, EventArgs e)
        {
            if (listBoxCSV.Items.Count == 0 || listBoxDOCX.Items.Count == 0 ||
                listBoxFOLDER.Items.Count == 0 || comboFileNameField.SelectedItem is null)
            {
                MessageBox.Show("Selecione o CSV, o template, a pasta de destino e o campo para nome do arquivo.");
                return;
            }

            var settings = new BatchSettings
            {
                CsvPath = listBoxCSV.Items[0]!.ToString()!,
                TemplatePath = listBoxDOCX.Items[0]!.ToString()!,
                OutputFolder = listBoxFOLDER.Items[0]!.ToString()!,
                FileNameField = comboFileNameField.SelectedItem!.ToString()!,
            };

            if (MessageBox.Show("Iniciar a geração dos certificados?", "Gerar Certificados",
                    MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            SetControlsEnabled(false);
            _cts = new CancellationTokenSource();
            var progress = new Progress<int>(value => progressBar.Value = Math.Min(value, progressBar.Maximum));

            try
            {
                var records = _dataSource.Read(settings.CsvPath);
                progressBar.Maximum = records.Count;
                progressBar.Value = 0;

                var results = await ProcessBatchAsync(records, settings, progress, _cts.Token);

                var failed = results.Count(r => !r.Success);
                MessageBox.Show(failed == 0
                    ? $"Processo concluído. {results.Count} certificado(s) gerado(s) em: {settings.OutputFolder}"
                    : $"Processo concluído com {failed} erro(s) de {results.Count}. Verifique a pasta de destino.");
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Operação cancelada.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
            finally
            {
                SetControlsEnabled(true);
                progressBar.Value = 0;
                _cts = null;
            }
        }

        private Task<List<CertificateResult>> ProcessBatchAsync(
            IReadOnlyList<CertificateRecord> records,
            BatchSettings settings,
            IProgress<int> progress,
            CancellationToken ct)
        {
            return Task.Run(() =>
            {
                var results = new List<CertificateResult>();
                var processed = 0;

                foreach (var record in records)
                {
                    ct.ThrowIfCancellationRequested();

                    try
                    {
                        var fileName = record.Get(settings.FileNameField) ?? Guid.NewGuid().ToString();
                        var docxPath = _documentBuilder.Build(settings.TemplatePath, record, settings.OutputFolder, fileName);

                        var pdfPath = Path.Combine(settings.OutputFolder, $"{fileName}.pdf");
                        _converter.ConvertToPdf(docxPath, pdfPath);
                        File.Delete(docxPath);

                        results.Add(CertificateResult.Ok(pdfPath));
                    }
                    catch (Exception ex)
                    {
                        results.Add(CertificateResult.Fail(ex.Message));
                    }

                    processed++;
                    progress.Report(processed);
                }

                return results;
            }, ct);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        private void SetControlsEnabled(bool enabled)
        {
            btnCsv.Enabled = enabled;
            btnDocx.Enabled = enabled;
            btnFolder.Enabled = enabled;
            btnGerar.Enabled = enabled;
            comboFileNameField.Enabled = enabled;
            btnCancelar.Enabled = !enabled;
        }
    }
}