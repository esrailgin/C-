﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics.Eventing.Reader;

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.accdb");

        private void kullanicilari_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicilari_listele = new OleDbDataAdapter(" select tcno AS [TC KİMLİK NO],ad AS [ADI],soyad AS [SOYADI],yetki AS [YETKİ],kullanici_adi AS [KULLANICI ADI],parola AS [PAROLA] from kullanicilar Order By ad ASC", baglantim);
                DataSet dshafiza = new DataSet();
                kullanicilari_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            { 
                MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close() ;
            }
        }
        private void personelleri_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter(" select tcno AS[TC KİMLİK NO],ad AS [ADI],soyad AS [SOYADI],cinsiyet AS [CİNSİYETİ],mezuniyet AS [MEZUNİYETİ],dogumtarihi AS [DOĞUM TARİHİ],gorevi AS [GÖREVİ], gorevyeri AS [GÖREV YERİ],maasi AS [MAAŞI] from personeller Order By ad ASC ", baglantim);
                DataSet dshafiza = new DataSet();
                personelleri_listele.Fill(dshafiza);
                dataGridView2.DataSource = dshafiza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Height = 150;
            pictureBox1.Width = 150;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.tcno + ".jpg");
            }
            catch(Exception)
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.jpg");
            }
            this.Text = "YÖNETİCİ İŞLEMLERİ";
            label10.ForeColor = Color.DarkRed;
            label10.Text = Form1.adi + " " + Form1.soyadi;
            textBox1.MaxLength = 11;
            textBox4.MaxLength = 8;
            toolTip1.SetToolTip(this.textBox1, "TC kimlik No 11 Karakter Olmalı!");
            radioButton1.Checked = true;

            textBox2.CharacterCasing=CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox5.MaxLength = 10;
            textBox6.MaxLength=10;
            progressBar1.Maximum= 100; 
            progressBar1.Value = 0;
            kullanicilari_goster();

            pictureBox2.SizeMode= PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 100;
            pictureBox2.Height = 100;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            maskedTextBox1.Mask = "00000000000";
            maskedTextBox2.Mask = "LL????????????????????";
            maskedTextBox3.Mask = "LL????????????????????";
            maskedTextBox4.Mask = "0000";
            maskedTextBox4.Text = "0";
            maskedTextBox2.Text.ToUpper();
            maskedTextBox3.Text.ToUpper();
            comboBox1.Items.Add("İlköğretim");
            comboBox1.Items.Add("Ortaöğretim");
            comboBox1.Items.Add("Lise");
            comboBox1.Items.Add("Üniversite");
            comboBox2.Items.Add("Yönetici");
            comboBox2.Items.Add("Memur");
            comboBox2.Items.Add("Şoför");
            comboBox2.Items.Add("İşçi");
            comboBox3.Items.Add("AR-GE");
            comboBox3.Items.Add("Bilgi İşlem");
            comboBox3.Items.Add("Muhasebe");
            comboBox3.Items.Add("Üretim");
            comboBox3.Items.Add("Paketleme");
            comboBox3.Items.Add("Nakliye");
            DateTime zaman = DateTime.Now;
            int yil = int.Parse(zaman.ToString("yyyy"));
            int ay = int.Parse(zaman.ToString("MM"));
            int gün = int.Parse(zaman.ToString("dd"));
            dateTimePicker1.MinDate = new DateTime(1960, 1, 1);
            dateTimePicker1.MaxDate = new DateTime(yil-18,ay,gün);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            radioButton3.Checked= true;
            personelleri_goster();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 11)
                errorProvider1.SetError(textBox1, "TC Kimlik No 11 Karakter Olmalı!");
            else
                errorProvider1.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
                e.Handled = false;

            else
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsLetter(e.KeyChar)==true || char.IsControl(e.KeyChar)==true || char.IsSeparator(e.KeyChar)==true)
                e.Handled = false;
            else 
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length > 8)
                errorProvider1.SetError(textBox1, "Kullanıcı Adı 8 Karakter Olmalı!");
            else
                errorProvider1.Clear();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }
        int parola_skoru = 0;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string parola_seviyesi = "";
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = textBox5.Text;
            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre;
            duzeltilmis_sifre = duzeltilmis_sifre.Replace("İ", "I");
            duzeltilmis_sifre = duzeltilmis_sifre.Replace("ı", "i");
            duzeltilmis_sifre = duzeltilmis_sifre.Replace("Ç", "C");
            duzeltilmis_sifre = duzeltilmis_sifre.Replace("ç", "c");
            duzeltilmis_sifre = duzeltilmis_sifre.Replace("Ş", "S");
            duzeltilmis_sifre = duzeltilmis_sifre.Replace("ş", "s");
            duzeltilmis_sifre = duzeltilmis_sifre.Replace("Ğ", "G");
            duzeltilmis_sifre = duzeltilmis_sifre.Replace("ğ", "g");
            duzeltilmis_sifre = duzeltilmis_sifre.Replace("Ü", "U");
            duzeltilmis_sifre = duzeltilmis_sifre.Replace("ü", "u");
            duzeltilmis_sifre = duzeltilmis_sifre.Replace("Ö", "O");
            duzeltilmis_sifre = duzeltilmis_sifre.Replace("ö", "o");
            if (sifre != duzeltilmis_sifre)
            {
                sifre= duzeltilmis_sifre;
                textBox5.Text = sifre;
                MessageBox.Show("Paroladaki Türkçe karakterler İngilizce karakterlere dönüştürülmüştür!");
            }
            int az_karakter_sayisi=sifre.Length-Regex.Replace(sifre, "[a-z]", "").Length;
            kucuk_harf_skoru = Math.Min(2, az_karakter_sayisi)*10;
            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, AZ_karakter_sayisi) * 10;
            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;
            int sembol_sayisi=sifre.Length-az_karakter_sayisi-AZ_karakter_sayisi-rakam_sayisi;
            sembol_skoru=Math.Min(2, sembol_sayisi) * 10;
            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;
            if (sifre.Length == 9)
                parola_skoru += 10;
            else if (sifre.Length == 10)
                parola_skoru += 20;
            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru==0 || rakam_skoru == 0 || sembol_skoru == 0)
                label23.Text = "Büyük harf, küçük harf, rakam ve sembol mutlaka kullnılmalıdır!";
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru !=0 && rakam_skoru != 0 && sembol_skoru != 0)
                label23.Text = "";
            if (parola_skoru < 70)
                parola_seviyesi = "Kabul edilemez!";
            else if (parola_skoru == 70 || parola_skoru == 80)
                parola_seviyesi = "Güçlü";
            else if (parola_skoru == 90 || parola_skoru == 100)
                parola_seviyesi = "Çok Güçlü";
            label11.Text = "%" + Convert.ToString(parola_skoru);
            label12.Text = parola_seviyesi;
            progressBar1.Value = parola_skoru;
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != textBox5.Text)
                errorProvider1.SetError(textBox6, "Parola tekrarı eşleşmiyor!");
            else
                errorProvider1.Clear();
        }
        private void topPage1_temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
        private void topPage2_temizle()
        {
            pictureBox2.Image = null;
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            maskedTextBox3.Clear();
            maskedTextBox4.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string yetki = "";
            bool kayitkontrol=false;
            baglantim.Open();
            OleDbCommand selectsorgu=new OleDbCommand("select*from kullanicilar where tcno '"+textBox1.Text+"",baglantim);
            OleDbDataReader kayitokuma =selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglantim.Close();
            if (kayitkontrol == false)
            {
                if(textBox1.Text.Length <11 || textBox1.Text=="")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;
                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;
                if (textBox4.Text.Length != 8 || textBox4.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Black;
                if(textBox5.Text==""||parola_skoru<70)
                    label6.ForeColor= Color.Red;
                else
                    label6.ForeColor=Color.Black;
                if (textBox6.Text == "" || textBox5.Text!=textBox6.Text)
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;
                if(textBox1.Text.Length==11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
                {
                    if (radioButton1.Checked == true)
                        yetki = "Yönetici";
                    else if (radioButton2.Checked == true)
                        yetki = "Kullanıcı";
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("Insert into kullanicilar values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + yetki + "','" + textBox4.Text + "','" + textBox5.Text + "')",baglantim);
                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        MessageBox.Show("Yeni kullanıcı kaydı oluşturuldu!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        topPage1_temizle();
                    }
                    catch(Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Girilen TC Kimlik Numarası Daha Önceden Kayıtlıdır!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool kayit_arama_durumu = false;
            if(textBox1.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("Select * from kullanicilar where tcno'" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while(kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    textBox2.Text = kayitokuma.GetValue(1).ToString();
                    textBox3.Text = kayitokuma.GetValue(2).ToString();
                    if (kayitokuma.GetValue(3).ToString() == "Yönetici")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                    textBox4.Text=kayitokuma.GetValue(4).ToString();
                    textBox5.Text = kayitokuma.GetValue(5).ToString();
                    textBox6.Text = kayitokuma.GetValue(5).ToString();
                    break;
                }
                if (kayit_arama_durumu == false)
                {
                    MessageBox.Show("Aranan kayıt bulunamadı!","SKY Personel Takip Programı",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();
                }
                else
                {
                    MessageBox.Show("Lütfen 11 haneli bir TC Kimlik No giriniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    topPage1_temizle();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string yetki = "";
            if (textBox1.Text.Length < 11 || textBox1.Text == "")
                label1.ForeColor = Color.Red;
            else
                label1.ForeColor = Color.Black;
            if (textBox2.Text.Length < 2 || textBox2.Text == "")
                label2.ForeColor = Color.Red;
            else
                label2.ForeColor = Color.Black;
            if (textBox3.Text.Length < 2 || textBox3.Text == "")
                label3.ForeColor = Color.Red;
            else
                label3.ForeColor = Color.Black;
            if (textBox4.Text.Length != 8 || textBox4.Text == "")
                label5.ForeColor = Color.Red;
            else
                label5.ForeColor = Color.Black;
            if (textBox5.Text == "" || parola_skoru < 70)
                label6.ForeColor = Color.Red;
            else
                label6.ForeColor = Color.Black;
            if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                label7.ForeColor = Color.Red;
            else
                label7.ForeColor = Color.Black;
            if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
            {
                if (radioButton1.Checked == true)
                    yetki = "Yönetici";
                else if (radioButton2.Checked == true)
                    yetki = "Kullanıcı";
                try
                {
                    baglantim.Open();
                    OleDbCommand guncellekomutu = new OleDbCommand("update kullanicilar set ad='"+textBox2.Text+ "',soyad='"+textBox3.Text+ "',yetki='"+yetki+ "',kullaniciadi='"+textBox4.Text+ "',parola'"+textBox5.Text+"'where tcno='"+textBox1.Text+"'", baglantim);
                    guncellekomutu.ExecuteNonQuery();
                    baglantim.Close();
                    MessageBox.Show("Kullanıcı bilgileri güncellendi!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    kullanicilari_goster();
                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message,"SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                }
            }
            else
            {
                MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 11)
            {
                bool kayit_arama_durumu=false;
                baglantim.Open();
                OleDbCommand selectsorgu=new OleDbCommand("select * from kullanicilar where tcno='"+textBox1.Text+"'",baglantim);
                OleDbDataReader kayitokuma=selectsorgu.ExecuteReader();
                while(kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    OleDbCommand deletesorgu=new OleDbCommand("delete from kullanicilar where tcno='"+textBox1.Text+"'",baglantim);
                    deletesorgu.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı kaydı silindi!","SKY Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    baglantim.Close();
                    kullanicilari_goster();
                    topPage1_temizle();
                    break;
                }
                if(kayit_arama_durumu==false)
                    MessageBox.Show("Silinecek kayıt bulunamadı!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
                topPage1_temizle();
            }
            else
                MessageBox.Show("Lütfen 11 karakterden oluşan bir TC Kimlik No giriniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            topPage1_temizle();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog resimsec = new OpenFileDialog();
            resimsec.Title = "Personel resmi seçiniz.";
            resimsec.Filter = "JPG Dosyalar(*.jpg | *.jpg";
            if(resimsec.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox2.Image=new Bitmap(resimsec.OpenFile());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            bool kayitkontrol = false;
            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while(kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglantim.Close();
            if (kayitkontrol == false)
            {
                if(pictureBox2.Image==null)
                    button6.ForeColor = Color.Red;
                else
                    button6.ForeColor = Color.Black;
                if(maskedTextBox1.MaskCompleted==false)
                    label14.ForeColor = Color.Red;
                else
                    label14.ForeColor = Color.Black;
                if (maskedTextBox2.MaskCompleted == false)
                    label15.ForeColor = Color.Red;
                else
                    label15.ForeColor = Color.Black;
                if (maskedTextBox3.MaskCompleted == false)
                    label16.ForeColor = Color.Red;
                else
                    label16.ForeColor = Color.Black;
                if(comboBox1.Text=="")
                    label18.ForeColor = Color.Red;
                else
                    label18.ForeColor = Color.Black;
                if (comboBox2.Text == "")
                    label20.ForeColor = Color.Red;
                else
                    label20.ForeColor = Color.Black;
                if (comboBox3.Text == "")
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;
                if (maskedTextBox4.MaskCompleted == false)
                    label22.ForeColor = Color.Red;
                else
                    label22.ForeColor = Color.Black;
                if(int.Parse(maskedTextBox4.Text) < 1000)
                    label22.ForeColor = Color.Red;
                else
                    label22.ForeColor = Color.Black;
                if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox1.MaskCompleted != false && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && maskedTextBox4.MaskCompleted != false)
                {
                    if (radioButton3.Checked == true)
                        cinsiyet = "Erkek";
                    else if (radioButton4.Checked == true) 
                        cinsiyet = "Kadın";
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("Insert Into personeller values('" + maskedTextBox1.Text + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + cinsiyet + "','" + comboBox1.Text + "','" + dateTimePicker1.Text + "','" + comboBox2 + "','" + comboBox3 + "','" + maskedTextBox4.Text + "')", baglantim);
                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        if (!Directory.Exists(Application.StartupPath + "\\personelresimler"))
                            Directory.CreateDirectory(Application.StartupPath + "\\personelresimler");
                        else
                            pictureBox2.Image.Save(Application.StartupPath + "\\personelresimler\\" + maskedTextBox1.Text+ ".jpg");
                        MessageBox.Show("Yeni personel kaydı oluşturuldu.","SKY Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        personelleri_goster();
                        topPage2_temizle();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglantim.Close();
                    }
                }
                else 
                { 
                    MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz!","SKY Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                
        }
            else
            {
                MessageBox.Show("Girilen TC Kimlik Numarası daha önceden kayıtlıdır!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool kayit_arama_durumu=false;
            if (maskedTextBox1.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where tcno='"+maskedTextBox1.Text+"'",baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while(kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    try
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + kayitokuma.GetValue(0).ToString() + ".jpg");
                    }
                    catch
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.jpg");
                    }
                    maskedTextBox2.Text=kayitokuma.GetValue(1).ToString();
                    maskedTextBox3.Text = kayitokuma.GetValue(2).ToString();
                    if(kayitokuma.GetValue(3).ToString()=="Erkek")
                        radioButton3.Checked=true;
                    else
                        radioButton4.Checked=true;
                    comboBox1.Text=kayitokuma.GetValue(4).ToString();
                    dateTimePicker1.Text=kayitokuma.GetValue(5).ToString();
                    comboBox2.Text = kayitokuma.GetValue(6).ToString();
                    comboBox3.Text = kayitokuma.GetValue(7).ToString();
                    maskedTextBox4.Text = kayitokuma.GetValue(8).ToString();
                    break;
                }
                if(kayit_arama_durumu==false)
                    MessageBox.Show("Aranan kayıt bulunamadı!","SKY Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                baglantim.Close();
            }
            else
            {
                MessageBox.Show("11 haneli TC Kimlik No giriniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            if (pictureBox2.Image == null)
                button6.ForeColor = Color.Red;
            else
                button6.ForeColor = Color.Black;
            if (maskedTextBox1.MaskCompleted == false)
                label14.ForeColor = Color.Red;
            else
                label14.ForeColor = Color.Black;
            if (maskedTextBox2.MaskCompleted == false)
                label15.ForeColor = Color.Red;
            else
                label15.ForeColor = Color.Black;
            if (maskedTextBox3.MaskCompleted == false)
                label16.ForeColor = Color.Red;
            else
                label16.ForeColor = Color.Black;
            if (comboBox1.Text == "")
                label18.ForeColor = Color.Red;
            else
                label18.ForeColor = Color.Black;
            if (comboBox2.Text == "")
                label20.ForeColor = Color.Red;
            else
                label20.ForeColor = Color.Black;
            if (comboBox3.Text == "")
                label21.ForeColor = Color.Red;
            else
                label21.ForeColor = Color.Black;
            if (maskedTextBox4.MaskCompleted == false)
                label22.ForeColor = Color.Red;
            else
                label22.ForeColor = Color.Black;
            if (int.Parse(maskedTextBox4.Text) < 1000)
                label22.ForeColor = Color.Red;
            else
                label22.ForeColor = Color.Black;
            if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox1.MaskCompleted != false && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && maskedTextBox4.MaskCompleted != false)
            {
                if (radioButton3.Checked == true)
                    cinsiyet = "Erkek";
                else if (radioButton4.Checked == true) 
                    cinsiyet = "Kadın";
                try
                {
                    baglantim.Open();
                    OleDbCommand guncellekomutu = new OleDbCommand("update personeller set ad='" + maskedTextBox2.Text + "',soyad='" + maskedTextBox3.Text + "',cinsiyet='" + cinsiyet + "',mezuniyet='" + comboBox1.Text + "',dogumtarihi='" + dateTimePicker1.Text + "',gorevi='" + comboBox2 + "'gorevyeri='" + comboBox3 + "',maasi='" + maskedTextBox4.Text + "'where tcno='"+maskedTextBox1.Text+"'", baglantim);
                    guncellekomutu.ExecuteNonQuery();
                    baglantim.Close();
                    personelleri_goster();
                    topPage2_temizle();
                    maskedTextBox4.Text = "0";
                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                }
            }
    }

        private void button10_Click(object sender, EventArgs e)
        {
            if(maskedTextBox1.MaskCompleted == true)
            {
                bool kayit_arama_durumu=false;
                baglantim.Open();
                OleDbCommand arama_sorgu = new OleDbCommand("select * from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma=arama_sorgu.ExecuteReader();
                while(kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    OleDbCommand deletesorgu=new OleDbCommand("delete from personeller where tcno='"+maskedTextBox1.Text+"'", baglantim);
                    deletesorgu.ExecuteNonQuery();
                    break;
                }
            
                if(kayit_arama_durumu == false)
                {
                    MessageBox.Show("Silinecek kayıt bulunamadı!", "SKY Personel Takip Programı",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglantim.Close() ;
                personelleri_goster();
                topPage2_temizle();
                maskedTextBox4.Text = "0";
            }
            else
            {
                MessageBox.Show("Lütfen 11 karakterden oluşan bir TC Kimlik Numarası giriniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage2_temizle();
                maskedTextBox4.Text = "0";
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            topPage2_temizle();
        }
    }
}
