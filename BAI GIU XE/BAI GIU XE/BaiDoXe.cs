using System;
using System.Collections.Generic;
using System.Text;

namespace BAI_GIU_XE
{
    
    public class MyEventArgs
    {
        public XeStatus xeStatus { set; get; }
        public barrierStatus data { set; get; }
        public string content1 { set; get; }
        public MyEventArgs(barrierStatus data, string content1, XeStatus xeStatus)
        {
            this.data = data;
            this.content1 = content1;
            this.xeStatus = xeStatus;
        }
    }
    class BaiDoXe
    {
        public string name { set; get; }
        public int soxemax { get; }
        public BangHienThi b0 { set; get; }
        public Server server { set; get; }
        public Barrier barrier1 { set; get; }
        public NhanVien nhanVien { set; get; }
        public BaiDoXe() { }
        public BaiDoXe(string name, int soxemax, Server server, BangHienThi banghienthi, Barrier barrier, NhanVien nhanVien)
        {
            this.name = name;
            this.soxemax = soxemax;
            this.server = new Server(server);
            this.server.soxemax = this.soxemax;
            this.b0 = new BangHienThi(banghienthi);
            this.barrier1 = new Barrier(barrier);
            this.nhanVien = new NhanVien(nhanVien);

        }
        public BaiDoXe(BaiDoXe x)
        {
            this.name = x.name;
            this.soxemax = x.soxemax;
            this.server = new Server(x.server);
            b0 = new BangHienThi(x.b0);
            barrier1 = new Barrier(x.barrier1);
            this.nhanVien = new NhanVien(x.nhanVien);
        }
        public delegate void InOutHandler(Xe b, MyEventArgs e);
        public event InOutHandler EVInOutHandler;
        public void Xevao(Xe a)
        {
            EVInOutHandler?.Invoke(a, new MyEventArgs(barrierStatus.CloseBarrier, "Moi xe ", XeStatus.vao));
        }
        public void Xera(Xe a)
        {
            EVInOutHandler?.Invoke(a, new MyEventArgs(barrierStatus.CloseBarrier, "Moi xe ", XeStatus.ra));
        }
        public delegate object evMoBai(params object[] thamso);
        public event evMoBai OPEN;
        public object moBai(params object[] thamso)
        {
            return OPEN?.Invoke(thamso);
        }
        public delegate object evDongBai(params object[] thamso);
        public event evDongBai CLOSE;
        public object dongBai(params object[] thamso)
        {
            return CLOSE?.Invoke(thamso);
        }
    }
}
