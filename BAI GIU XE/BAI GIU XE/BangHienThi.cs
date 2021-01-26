using System;
using System.Collections.Generic;
using System.Text;

namespace BAI_GIU_XE
{
    class BangHienThi
    {              
        public string bienSo;
        public string khu;
        public int hang;
        public string viTri;

        public BangHienThi() { }
        public BangHienThi(string bs,string k,int h,string vt)
        {
            this.bienSo = bs;
            this.khu = k;
            this.hang = h;
            this.viTri = vt;
        }
        public BangHienThi(BangHienThi x)
        {
            this.bienSo = x.bienSo;
            this.khu = x.khu;
            this.hang = x.hang;
            this.viTri = x.viTri;
        }
    }
}
