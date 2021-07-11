using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Uyg1TelDefteri
{
    class DBIslemleri
    {
        static string baglantiYolu = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mgoek\Downloads\11dersss\g2\db\TelDefteri2.mdf;Integrated Security=True;Connect Timeout=30";
        static SqlConnection baglanti = new SqlConnection(baglantiYolu);
        public static DataSet ulkeleriCek()
        {
            string sql = "select * from Ulkeler";
            SqlCommand komut = new SqlCommand();
            komut.CommandText = sql;
            komut.Connection = baglanti;

            SqlDataAdapter adaptor = new SqlDataAdapter();
            adaptor.SelectCommand = komut;
            DataSet ulkelerDS = new DataSet();
            baglanti.Open();
            adaptor.Fill(ulkelerDS);
            baglanti.Close();
            return ulkelerDS;

        }

        public static DataSet sehirleriCek(int ulkeID)
        {
            string sql = "select Sehir from Sehirler where UlkeID=" + ulkeID;
            SqlCommand komut = new SqlCommand();
            komut.CommandText = sql;
            komut.Connection = baglanti;

            SqlDataAdapter adaptor = new SqlDataAdapter();
            adaptor.SelectCommand = komut;

            DataSet sehirlerDS = new DataSet();
            baglanti.Open();
            adaptor.Fill(sehirlerDS);
            baglanti.Close();
            return sehirlerDS;

        }

        public static void Ekle(string ad, string soyad, string tel, int sid, string adr)
        {
            //string sql = "insert into Kisiler values ('"+ad+"','"+soyad+"','"+tel+"',"+sid+",'"+adr+"')";
            string sql = "insert into Kisiler values (@pAd,@pSoyad,@pTel,@pSid,@pAdr)";
            SqlCommand komut = new SqlCommand();
            komut.CommandText = sql;
            komut.Connection = baglanti;

            komut.Parameters.AddWithValue("@pAd", ad);
            komut.Parameters.AddWithValue("@pSoyad", soyad);
            komut.Parameters.AddWithValue("@pTel", tel);
            komut.Parameters.AddWithValue("@pSid", sid);
            komut.Parameters.AddWithValue("@pAdr", adr);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();


        }//ekle

        public static DataSet Arama(string ad)
        {
            string sql = "Select * from Kisiler where Adi like @pAd+'%'";
            SqlCommand komut = new SqlCommand();
            komut.CommandText = sql;
            komut.Connection = baglanti;
            komut.Parameters.AddWithValue("@pAd", ad);

            SqlDataAdapter adaptor = new SqlDataAdapter();
            adaptor.SelectCommand = komut;

            DataSet sonuclar = new DataSet();
            baglanti.Open();
            adaptor.Fill(sonuclar);
            baglanti.Close();
            return sonuclar;

        }

        public static void Guncelle(int kisiID,string tel, string adr)
        {
            string sql = "update Kisiler set Telefon=@pTel, Adres=@pAdr where KisiID=@pkid";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            komut.Parameters.AddWithValue("@pTel", tel);
            komut.Parameters.AddWithValue("@pAdr", adr);
            komut.Parameters.AddWithValue("@pkid", kisiID);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

        }

        public static void Sil(int kisiID)
        {
            string sql = "delete from Kisiler where KisiID=" + kisiID;
            SqlCommand komut = new SqlCommand(sql, baglanti);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
        }


    }
}
