using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Diski doldurmak için dosya yolu ve boyutunu belirle
  Console.WriteLine("Dosya yolunu belirtiniz.");
    

        string dosyaYolu = Console.ReadLine();
          DriveInfo[] allDrives = DriveInfo.GetDrives();

        foreach (DriveInfo d in allDrives)
        {
            Console.WriteLine($"{d.Name}");
        }
        Console.WriteLine("Eğer tüm boş alanları doldurmak isterseniz drive adını yazınız. Aksi taktirde boş bırakınız(enter)");
        string driveName = Console.ReadLine();

long dosyaBoyutuByte = 0;

            Console.WriteLine($"Drive Name : {driveName}");
      Console.ReadLine();
        if(!string.IsNullOrWhiteSpace(driveName))
        {
            long totalFreeSpaceInBytes = GetTotalFreeSpace(driveName) ;
            Console.WriteLine($"Toplam {totalFreeSpaceInBytes} byte MB yer var. Devam etmek için enter tuşuna basınız");
    Console.ReadLine();
            dosyaBoyutuByte = totalFreeSpaceInBytes ;

        }else{
        Console.WriteLine("MB cinsinden oluşturulacak dosya boyutu?");
        dosyaBoyutuByte = Convert.ToInt32(Console.ReadLine());
        }
        // Dosyayı oluştur ve rastgele veriyle doldur
        OluşturVeDoldur(dosyaYolu, dosyaBoyutuByte);

        Console.WriteLine("Dosya oluşturuldu ve disk dolduruldu.");
    }

    static void OluşturVeDoldur(string dosyaYolu, long boyutByte)
    {
        try
        {
            using (FileStream fs = new FileStream(dosyaYolu, FileMode.Create, FileAccess.Write))
            {
                Random rastgele = new Random();

                byte[] buffer = new byte[1024]; // 1 KB'lık bir arabellek oluştur
                long toplamByte = boyutByte ;

                while (toplamByte > 0)
                {
                    rastgele.NextBytes(buffer); // Rastgele veri üret

                    long yazilacakByte = Math.Min(toplamByte, buffer.Length); // Kalan byte'ı kontrol et

                    fs.Write(buffer, 0, (int)yazilacakByte); // Dosyaya yaz
                    toplamByte -= yazilacakByte; // Kalan byte'ı güncelle
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
        }
    }

    private static long GetTotalFreeSpace(string driveName)
{
    foreach (DriveInfo drive in DriveInfo.GetDrives())
    {
        if (drive.IsReady && drive.Name == driveName)
        {
            return drive.TotalFreeSpace;
        }
    }
    return -1;
}
}
