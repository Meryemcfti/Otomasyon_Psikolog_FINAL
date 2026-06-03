# Psikolog Danışan ve Randevu Yönetim Sistemi

## Öğrenci Bilgileri

**Ad Soyad:** Meryem Çiftçi
**Okul No:** 2312503065

---

## Proje Hakkında

Bu proje, Nesne Tabanlı Programlama dersi kapsamında geliştirilmiş bir psikolog danışan ve randevu yönetim sistemidir.

Projenin amacı, psikologların danışan bilgilerini ve randevu süreçlerini daha düzenli bir şekilde yönetebilmesini sağlamaktır. Sistem üzerinden danışan kayıtları tutulabilmekte, randevular oluşturulabilmekte ve mevcut kayıtlar üzerinde güncelleme veya silme işlemleri yapılabilmektedir.

Projeyi geliştirirken kullanıcı dostu ve sade bir arayüz oluşturmaya dikkat edilmiştir. Böylece kullanıcıların sistem üzerinde işlemlerini daha kolay gerçekleştirmesi hedeflenmiştir.

---

## Projede Kullanılan Teknolojiler

* C#
* Windows Forms
* .NET Framework 4.7.2
* Microsoft SQL Server
* Entity Framework 6
* LINQ
* Git & GitHub

---

## Sistem Özellikleri

### Kullanıcı Girişi

Sisteme giriş yapılabilmesi için kullanıcı adı ve şifre kontrolü bulunmaktadır.

### Danışan Yönetimi

* Danışan ekleme
* Danışan güncelleme
* Danışan silme
* Danışan listeleme

### Randevu Yönetimi

* Yeni randevu oluşturma
* Randevu bilgilerini görüntüleme
* Randevu güncelleme
* Randevu silme

### Seans Yönetimi

Danışanlara farklı seans türleri atanabilmektedir.

### Veri Tabanı İşlemleri

Tüm veriler SQL Server veritabanında saklanmaktadır. Veritabanı işlemlerinde Entity Framework kullanılmıştır.

---

## Veritabanı Yapısı

Projede aşağıdaki temel tablolar kullanılmaktadır:

### Customer

Danışan bilgilerini tutmaktadır.

* Ad 
* Soyad
* Telefon
* E-posta

### Appointment

Randevu bilgilerini tutmaktadır.

* Randevu Tarihi
* Randevu Saati
* Danışan Bilgisi
* Seans Türü

### SessionType

Seans türlerini tutmaktadır.

Örnek:

* Bireysel Terapi
* Çift Terapisi
* Aile Terapisi

### Department

Bölüm bilgilerini tutmaktadır.

---

## Nesne Tabanlı Programlama Yapıları

Bu projede nesne tabanlı programlama prensipleri uygulanmıştır.

Kullanılan temel yapılar:

* Class yapıları
* Encapsulation
* Entity sınıfları
* Navigation Property kullanımı
* Katmanlı veri erişimi
* Entity Framework Code First yaklaşımı

---

## Proje Ekranları

Projede aşağıdaki ekranlar bulunmaktadır:

* Login Formu
* Danışan Yönetim Ekranı
* Randevu Yönetim Ekranı

---

## Proje Amacı

Bu proje ile hem nesne tabanlı programlama mantığının uygulanması hem de gerçek hayatta kullanılabilecek basit bir otomasyon sisteminin geliştirilmesi amaçlanmıştır.

Proje geliştirme sürecinde C#, SQL Server, Entity Framework ve Windows Forms teknolojileri kullanılarak veritabanı bağlantıları, form işlemleri ve CRUD (Create, Read, Update, Delete) işlemleri uygulanmıştır.

---

## GitHub

Bu proje eğitim amaçlı olarak geliştirilmiştir.

Nesne Tabanlı Programlama Dersi Final Projesi.
