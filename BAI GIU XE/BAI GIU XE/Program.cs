using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAI_GIU_XE
{
    
    public class Program
    {
        
        static void Main(string[] args)
        {
            Xe a = new Xe("345", "small", 20000, PhanLoaiXe.XeOto);
            Xe b = new Xe("34325", "small", 2000, PhanLoaiXe.XeMay);
            /* Xe b = new Xe();
            b.bienSoXe = Console.ReadLine();
            b.hd = Console.ReadLine();
            b.tienguixe = int.Parse(Console.ReadLine());
            string temp = Console.ReadLine();
            if (temp == "0") b.loaiXe = PhanLoaiXe.XeDap;
            else if (temp == "1") b.loaiXe = PhanLoaiXe.XeMay;
            else b.loaiXe = PhanLoaiXe.XeOto; */
            
            //Thanh phan cua bai xe
            Server sv = new Server(500);
            NhanVien staff = new NhanVien();
            Barrier frontGate = new Barrier();
            BangHienThi bang1 = new BangHienThi();
            BaiDoXe baidx1 = new BaiDoXe("SPKT", 10, sv, bang1, frontGate, staff);
            //event Xe check in & out
            baidx1.EVInOutHandler += NhanVienLog;
            baidx1.EVInOutHandler += baidx1.server.check;
            baidx1.EVInOutHandler += BangHienThiLog;
            baidx1.EVInOutHandler += baidx1.barrier1.BarrierAct;
            baidx1.EVInOutHandler += BarrierLog;

            baidx1.OPEN += EV_OPEN;
            Console.WriteLine(baidx1.moBai()); //event mo bai do xe
            //baidx1.Xevao(a);            
            b.In += EV_In; 
            Console.WriteLine(b.thongBaoXeVao(b)); //event thong bao xe vao
            baidx1.Xevao(b);
            //baidx1.Xera(a);
            b.Out += EV_Out;
            Console.WriteLine(b.thongBaoXeRa(b)); //event thong bao xe ra
            baidx1.Xera(b);
            //event cai dat 1 camera moi vao bai giu xe
            Camera i = new Camera();
            i.viTriCamera = "Khu a";
            staff.EVInstallCam += I_EVInstallCam;
            Console.WriteLine(staff.thucThiEVInstallCam(i));

            //event bao tri camera
            staff.evMaintainCam += _evMaintainCam;
            Console.WriteLine(staff.thucThiEVMainTainCam(i));

            //event nhan vien tinh tien thoi va tra cho khach
            staff.EVTinhtienth += Staff_EVTinhtienth;
            Console.WriteLine(staff.thucThiEVTinhTienth(sv, b));
            staff.maNhanVien = "hxp";
            staff.NgayDiemdanh = "20";
            staff.songaydadiemdanh = 29;
            staff.NVDiemdanhNgaylamviec(sv);
            foreach (KeyValuePair<string,string> item in sv.DSdiemdanhh)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
            //event phat luong
            sv.EVphatLuong += Sv_EVphatLuong;
            sv.thucthiEVphatLuong(staff);

            //event check so ngay diem danh sau khi phat luong
            sv.EVChPhatLuong += Sv_EVChPhatLuong;
            sv.thucthiEVChPhatLuong(staff);

            baidx1.CLOSE += EV_CLOSE;
            Console.WriteLine(baidx1.dongBai()); //event dong bai do xe

            staff.EVMaintainSV += _EVMaintainSV;
            Console.WriteLine(staff.thucThiEVMaintainSV(sv));
        }

        private static object _EVMaintainSV(Server a)
        {
            return "Nhan vien tien hanh bao tri server";
        }

        private static object _evMaintainCam(Camera a)
        {
            return $"Nhan Vien: Bao tri camera tai vi tri {a.viTriCamera}";
        }

        private static object EV_CLOSE(params object[] thamso)
        {
            return "Dong bai do xe";
        }

        private static object EV_OPEN(params object[] thamso)
        {
            return "Mo bai do xe";
        }

        private static object EV_Out(Xe a)
        {
            return $"Xe co bien so {a.bienSoXe} ra";
        }

        private static object EV_In(Xe a)
        {
            return $"Xe co bien so {a.bienSoXe} vao";
        }

        private static void Sv_EVChPhatLuong(NhanVien a)
        {
            if (a.StatusNhanLuong == LuongStatus.chua_nhan)
            {
                a.songaydadiemdanh = 0;
                Console.WriteLine("Nhan vien nay da nhan luong roi");
                a.StatusNhanLuong = LuongStatus.da_nhan;
            }
            else Console.WriteLine("Nhan vien nay van chua duoc nhan luong");
        }

        private static void Sv_EVphatLuong(NhanVien a)
        {
            if (a.songaydadiemdanh >= 28)
            {
                Console.WriteLine("Sever: Luong nhan vien nhan duoc la " + (a.Luongcoban * 2) + " VND");
                a.StatusNhanLuong = LuongStatus.da_nhan;
            }
            else if (a.songaydadiemdanh >= 15)
            {
                Console.WriteLine("Sever: Luong nhan vien nhan duoc la " + (a.Luongcoban * 1.5) + " VND");
                a.StatusNhanLuong = LuongStatus.da_nhan;
            }
            else
            {
                Console.WriteLine("Sever: Luong nhan vien nhan duoc la " + (a.Luongcoban * 0.5) + " VND");
                a.StatusNhanLuong = LuongStatus.da_nhan;
            }
        }

        private static object Staff_EVTinhtienth(Server a, Xe b)
        {
            if (a.tienphaitra(b) < b.tienguixe) return $"Nhan Vien: Tra lai khach:{b.tienguixe - a.tienphaitra(b)}\n";
            else return "";
        }
        private static object I_EVInstallCam(Camera a)
        {
            return "Nhan Vien: Cai dat camera tai vi tri " + a.viTriCamera;
        }

        public static void NhanVienLog(Xe a, MyEventArgs e)
        {
            if(e.xeStatus == XeStatus.vao)   
                Console.WriteLine($"Nhan vien: Kiem tra cho trong cho xe {a.bienSoXe}");
            else
                Console.WriteLine($"Nhan vien: Kiem tra the cua xe {a.bienSoXe}");
        }
        public static void BangHienThiLog(Xe a, MyEventArgs e)
        {
            if (e.content1 == "The xe khong hop le giu xe " || e.content1 == "Khach khong dua du tien nen giu xe ") Console.WriteLine($"Bang hien thi: {e.content1}{a.bienSoXe}");
            else Console.WriteLine($"Bang hien thi: {e.content1}{a.bienSoXe} {e.xeStatus.ToString()}");
        }
        public static void BarrierLog(Xe a, MyEventArgs e)
        {
            Console.WriteLine($"Barrier: {e.data}");
        }
      
    }
}
