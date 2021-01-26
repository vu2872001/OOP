using System;
using System.Collections.Generic;
using System.Text;

namespace BAI_GIU_XE
{
    public enum  PhanLoaiXe
    {
        XeDap,
        XeMay,
        XeOto
    }
    public enum XeStatus 
    { 
      vao,
      ra
    }
    public class Xe
    {
        public string bienSoXe { set; get; }
        public string hd { set; get; }
        //dùng bienso làm ID luôn        
        public string mauXe { set; get; }
        public PhanLoaiXe loaiXe { set; get; }
        public DateTime ngayGui { set; get; }
        public DateTime soGioDuocPhepGui { set; get; }
        public int tienguixe { set; get; }
        
        public Xe() { }
        public Xe(string biensoxe, string hd,int tiengui, PhanLoaiXe lx) 
        {
            this.bienSoXe = biensoxe;
            this.hd = hd;
            this.tienguixe = tiengui;
            this.loaiXe = lx;
        }
        public Xe(string bienSoXe,string hd, string mauXe, PhanLoaiXe loaiXe, DateTime ngayGui, DateTime soGioDuocPhep,int tiengui)
        {
            this.bienSoXe = bienSoXe;
            this.hd = hd;
            this.mauXe = mauXe;
            this.loaiXe = loaiXe;
            this.ngayGui = ngayGui;
            this.soGioDuocPhepGui = soGioDuocPhep;
            this.tienguixe = tiengui;
        }
        public Xe(Xe lamboghini)
        {
            this.bienSoXe = lamboghini.bienSoXe;
            this.hd = lamboghini.hd;
            this.mauXe = lamboghini.mauXe;
            this.loaiXe = lamboghini.loaiXe;
            this.ngayGui = lamboghini.ngayGui;
            this.soGioDuocPhepGui = lamboghini.soGioDuocPhepGui;
            this.tienguixe = lamboghini.tienguixe;
        }
        public delegate object DelegateThongBaoXeVao(Xe a);
        public event DelegateThongBaoXeVao In;
        public object thongBaoXeVao(Xe a)
        {
            return In?.Invoke(a);
        }
        public delegate object DelegateThongBaoXeRa(Xe a);
        public event DelegateThongBaoXeRa Out;
        public object thongBaoXeRa(Xe a)
        {
            return Out?.Invoke(a);
        }
    }
}
