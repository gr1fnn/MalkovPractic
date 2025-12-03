// Простое окно для графика - добавьте этот класс
public class SimplePlotForm : Form
{
    private PictureBox pictureBox;

    public SimplePlotForm(Image image)
    {
        InitializeForm();
        ShowImage(image);
    }

    private void InitializeForm()
    {
        this.Text = "График модели";
        this.Size = new Size(600, 500);
        this.StartPosition = FormStartPosition.CenterParent;

        // Панель с прокруткой
        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true
        };

        pictureBox = new PictureBox
        {
            SizeMode = PictureBoxSizeMode.AutoSize
        };

        panel.Controls.Add(pictureBox);
        this.Controls.Add(panel);

        // Кнопка сохранения
        var saveButton = new Button
        {
            Text = "Сохранить",
            Size = new Size(80, 30),
            Location = new Point(10, 10)
        };
        saveButton.Click += (s, e) => SaveImage();
        this.Controls.Add(saveButton);
    }

    private void ShowImage(Image image)
    {
        if (image != null)
        {
            pictureBox.Image = image;

            // Центрируем изображение
            pictureBox.Location = new Point(
                Math.Max(0, (pictureBox.Parent.Width - pictureBox.Width) / 2),
                Math.Max(0, (pictureBox.Parent.Height - pictureBox.Height) / 2)
            );
        }
        else
        {
            pictureBox.Image = CreateErrorImage();
        }
    }

    private Image CreateErrorImage()
    {
        var bitmap = new Bitmap(400, 200);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.Clear(Color.White);
            g.DrawString("Невозможно построить график",
                new Font("Arial", 12, FontStyle.Bold),
                Brushes.Red, new PointF(50, 50));
            g.DrawString("Проверьте, что модель обучена",
                new Font("Arial", 10),
                Brushes.Black, new PointF(50, 80));
        }
        return bitmap;
    }

    private void SaveImage()
    {
        if (pictureBox.Image == null) return;

        using (SaveFileDialog dialog = new SaveFileDialog())
        {
            dialog.Filter = "PNG файлы|*.png|JPEG файлы|*.jpg";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image.Save(dialog.FileName);
                MessageBox.Show("Сохранено!");
            }
        }
    }
}