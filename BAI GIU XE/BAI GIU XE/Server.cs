using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BAI_GIU_XE
{
    
    public class Server
    {
        public Dictionary<string,string> DSdiemdanhh { set; get; }
        public Dictionary<string, string> UserData { set; get; }
        //dictionary với key là bienSo, value là image
        
        public int soxeht { set; get; } //đếm xe là việc của server
        public int soxemax { get; set; }
        public Camera cameras { set;  get; }
        public List<Xe> dsxe1 { set; get; }
        public Server() { }
        public Server(int soxemax)
        {
            this.soxemax = soxemax;
            soxeht = 0;
            this.DSdiemdanhh = new Dictionary<string, string>();
            this.UserData = new Dictionary<string, string>();
            this.cameras = new Camera("CAM01", "F");
            dsxe1 = new List<Xe>();
        }
        public Server(Server x)
        {
            this.DSdiemdanhh = x.DSdiemdanhh;
            this.soxemax = x.soxemax;
            this.soxeht = x.soxeht;
            this.UserData = x.UserData;
            this.cameras = new Camera(x.cameras);
            this.dsxe1 = x.dsxe1;
        }
        
        public void BarrierAction(Barrier sender, MyEventArgs e)
        {
            sender.status = e.data;
        }
        public bool kiemTraDuLieu(Xe a)
        {
            foreach (Xe item in this.dsxe1)
            {
                if (this.dsxe1.Contains(a)) return true;
            }
            return false;
        }
        public bool checkChoTrong()
        {
            return soxeht < soxemax;
        }
        public int tienphaitra(Xe a)
        {
            if (a.loaiXe == PhanLoaiXe.XeDap) return 2000;
            else if (a.loaiXe == PhanLoaiXe.XeMay) return 4000;
            else return 10000;
        }
        public void check(Xe a, MyEventArgs e)
        {

            //Check xem bai còn chổ ko --->kiểm tra sl xe trong đó
            if(e.xeStatus==XeStatus.vao)//tới GH hay chưa
            {
                
                if (this.checkChoTrong())
                {
                    //chup hinh --->event{
                    this.cameras.chupAnh(a);
                    //Luu xuong server
                    dsxe1.Add(a);
                    this.soxeht++;
                    a.ngayGui = DateTime.Now;
                    e.content1 = "Van con cho, moi xe ";
                    e.data = barrierStatus.OpenBarrier;
                }
                else
                {
                    e.content1 = "Da het cho, moi xe ";
                    e.xeStatus = XeStatus.ra;
                    e.data = barrierStatus.CloseBarrier;
                }
            }
            else
            {
                // kiem tra the xe co luu trong sever hay ko
                if (this.kiemTraDuLieu(a))
                {
                    if (this.tienphaitra(a) <= a.tienguixe)
                    {
                        this.soxeht--;
                        e.data = barrierStatus.OpenBarrier;
                    }
                    else
                    {
                        e.content1 = "Khach khong dua du tien nen giu xe ";
                        e.data = barrierStatus.CloseBarrier;
                    }
                }
                else
                {
                    e.content1 = "The xe khong hop le giu xe ";
                    e.data = barrierStatus.CloseBarrier;
                }
            }
        }
        public void DiemdanhNV(NhanVien a)
        {
            this.DSdiemdanhh.Add(a.maNhanVien, a.NgayDiemdanh); 
            a.songaydadiemdanh++;
        }
        public delegate void DeleGatePhatLuong(NhanVien a);
        public event DeleGatePhatLuong EVphatLuong;
        public void thucthiEVphatLuong(NhanVien a)
        {
             EVphatLuong?.Invoke(a);
        }
        public delegate void DeleGateCheckPhatLuong(NhanVien a);
        public event DeleGateCheckPhatLuong EVChPhatLuong;
        public void thucthiEVChPhatLuong(NhanVien a)
        {
             EVChPhatLuong?.Invoke(a);
        }
    }
}
