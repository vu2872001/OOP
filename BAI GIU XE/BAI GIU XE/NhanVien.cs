using System;
using System.Collections.Generic;
using System.Text;

namespace BAI_GIU_XE
{
    public enum LuongStatus { da_nhan,chua_nhan}
    public class NhanVien
    {
        public LuongStatus StatusNhanLuong { set; get; }
        public float Luongcoban { set; get; }
        public string tenNhanVien { set; get; }
        public string maNhanVien { set; get; }
        public string gioiTinh { set; get; }
        public string NgayDiemdanh { set; get; }
        public int songaydadiemdanh { set; get; }
        public string noiDungCV { set; get; }
        public NhanVien()
        {
            this.Luongcoban = 500000;
            this.StatusNhanLuong = LuongStatus.chua_nhan;
        }
        public NhanVien(string tenNV, string maNV, string sex, int songaydd, string Ngaydd, string noiDungCV)
        {  this.tenNhanVien = tenNV;
            this.maNhanVien = maNV;
            this.gioiTinh = sex;
            this.songaydadiemdanh = songaydd;
            this.NgayDiemdanh = Ngaydd;
            this.noiDungCV = noiDungCV;
            this.Luongcoban = 500000;
            this.StatusNhanLuong = LuongStatus.chua_nhan;
        }
        public NhanVien(NhanVien x)
        {
            this.tenNhanVien = x.tenNhanVien;
            this.maNhanVien = x.maNhanVien;
            this.gioiTinh = x.gioiTinh;
            this.songaydadiemdanh = x.songaydadiemdanh;
            this.noiDungCV = x.noiDungCV;
            this.NgayDiemdanh = x.NgayDiemdanh;
            this.Luongcoban = x.Luongcoban;
            this.StatusNhanLuong = x.StatusNhanLuong;
        }
        public string Giuxelai(Xe a)
        {
            return "Giu xe " + a.bienSoXe + "\n";
        }
     /*   public string tratienthua(Server a, Xe b)
        {
            if (a.tienphaitra(b) == b.tienguixe)
                return "\n";
            else return $"Tra lai khach:{b.tienguixe - a.tienphaitra(b)}\n";
        } */
        public delegate object DelegateTinhtienthoi(Server a, Xe b);
        public event DelegateTinhtienthoi EVTinhtienth;
        public object thucThiEVTinhTienth(Server a, Xe b)
        {
            return EVTinhtienth?.Invoke(a, b);
        }
        public void NVDiemdanhNgaylamviec(Server x)
        {
            x.DiemdanhNV(this);
        }
        public delegate object DelegateInstall(Camera a);
        public event DelegateInstall EVInstallCam;
        public object thucThiEVInstallCam(Camera a)
        {
            return EVInstallCam?.Invoke(a);
        }
        public delegate object DelegateMaintainCam(Camera a);
        public event DelegateMaintainCam evMaintainCam;
        public object thucThiEVMainTainCam(Camera a)
        {
            return evMaintainCam?.Invoke(a);
        }
        public delegate object DelegateMaintainSV(Server a);
        public event DelegateMaintainSV EVMaintainSV;
        public object thucThiEVMaintainSV(Server a)
        {
            return EVMaintainSV?.Invoke(a);
        }
    }
}
