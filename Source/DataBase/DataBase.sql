USE master;
GO
ALTER DATABASE QUAN_LY_QUAN_CAPHE SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO
drop database QUAN_LY_QUAN_CAPHE
create database QUAN_LY_QUAN_CAPHE
go
use QUAN_LY_QUAN_CAPHE
go

create table NhanVien
(
	MANV varchar(30) primary key,
	HOTEN nvarchar(100) NOT NULL,
	LUONG float,
	NGSINH date,    
	PHAI nvarchar(5) Check (PHAI IN('Nam','Nữ')),                                                                           
	CMND varchar(30),
	DIACHI nvarchar(100),
	DIENTHOAI varchar(30),
	CHUCVU nvarchar(30),
	TAIKHOAN varchar(30),
	MATKHAU varchar(50),
	ISDEL int,
	CREADTEDAT datetime,
	UPDATEDAT datetime
)

create table MonAn
(
	MAMON varchar(30) primary key,
	TENMON nvarchar(30),
	GIA float,
	MOTA nvarchar(30),
	MALOAI varchar(30),
	HINHANH image,
	TTSP nvarchar(30),
	ISDEL int,
	CREADTEDAT datetime,
	UPDATEDAT datetime
)
create table LoaiMonAn
(
	MALOAI varchar(30) primary key,
	TENLOAI nvarchar(30),
	ISDEL int,
	CREADTEDAT datetime,
	UPDATEDAT datetime
)
create table DonHang
(
	MADH varchar(30) primary key,
	MANV varchar(30),
	THOIGIAN datetime,
	TONGTIEN float,
	TENKH nvarchar(30),
	ISDEL int,
	CREADTEDAT datetime,
	UPDATEDAT datetime
)
create table ChiTietDonhang
(
	MADH VARCHAR(30) not null,
	MAMON VARCHAR(30) not null,
	SOLUONG  FLOAT,
	DONGIA FLOAT ,
	THANHTIEN  FLOAT,
	GIAMGIA FLOAT,
	ISDEL int,
	CREADTEDAT datetime,
	UPDATEDAT datetime
)
create table LichLamViec
(
	MANV  varchar(30) not null,
	THU nvarchar(30) not null,
	MACALV varchar(30) not null,
	PHUCAP float,
	ISDEL int,
	CREADTEDAT datetime,
	UPDATEDAT datetime
)
create table CaLamViec
(
	MACALV varchar(30) primary key,
	TENCA nvarchar(30),
	GIOBATDAU time,
	GIOKETTHUC time,
	ISDEL int,
	CREADTEDAT datetime,
	UPDATEDAT datetime
)
--tạo khóa ngoại cho bảng đơn hàng
alter table DonHang add
	constraint FK_DH_NV foreign key (MANV)
	references NhanVien (MANV)
GO	
-- tạo khóa chính, ngoại cho bảng lịch làm việc
alter table LichLamViec add 
	PRIMARY KEY(MANV,THU,MACALV)
 
 alter table LichLamViec add
	constraint FK_LLV_CLV foreign key (MACALV)
	references CaLamViec (MACALV)
GO	
 
 alter table LichLamViec add
	constraint FK_LLV_NV foreign key (MANV)
	references NhanVien (MANV)
GO	
-- tạo khóa chính, ngoại cho bảng chi tiết đơn hàng
alter table ChiTietDonHang add 
	PRIMARY KEY(MADH,MAMON)

alter table ChiTietDonHang add
	constraint FK_CTDH_DH foreign key (MADH)
	references DonHang (MADH)
GO	
alter table ChiTietDonHang add
	constraint FK_CTDH_MA foreign key (MAMON)
	references MonAn (MAMON)
GO	
-- tạo khóa ngoại cho bảng món ăn
alter table MonAn add
	constraint FK_MA_LMA foreign key (MALOAI)
	references LoaiMonAn (MALOAI)
GO	


insert into NhanVien (MANV,HOTEN,LUONG,NGSINH,PHAI,DIACHI,CMND,DIENTHOAI,CHUCVU,TAIKHOAN ,MATKHAU,ISDEL,CREADTEDAT,UPDATEDAT)
values ('NV01',N'Trương Văn Tú',2000,CAST(N'1959-06-20' AS Date),'Nam',N'167 Lương Nhữ Học Phường 11 Quận 5 HCM','123456','0978966563','Admin','admin','db69fc039dcbd2962cb4d28f5891aae1',1,CAST(N'1959-06-20' AS Date),CAST(N'1959-06-20' AS Date))



