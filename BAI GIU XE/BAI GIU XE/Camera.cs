using System;
using System.Collections.Generic;
using System.Text;

namespace BAI_GIU_XE
{
    public class Camera
    {
        public string maCamera { set; get; }
        public string viTriCamera { set; get; }

        public Xe xe { set; get; }
        public Camera() { }
        public Camera(string macam, string vt)
        {
            this.maCamera = macam;
            this.viTriCamera = vt;
        }
        public Camera(Camera x)
        {
            this.maCamera = x.maCamera;
            this.viTriCamera = x.viTriCamera;
        }
        public void chupAnh(Xe lamboghini)
        {
            xe = new Xe();
            xe.bienSoXe = lamboghini.bienSoXe;
            xe.hd = lamboghini.hd;
            xe.mauXe = lamboghini.mauXe;
            xe.loaiXe = lamboghini.loaiXe;
            xe.ngayGui = lamboghini.ngayGui;
            xe.soGioDuocPhepGui = lamboghini.soGioDuocPhepGui;
            xe.tienguixe = lamboghini.tienguixe;
        }       
    }
}
