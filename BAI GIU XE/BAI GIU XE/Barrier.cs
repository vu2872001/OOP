using System;
using System.Collections.Generic;
using System.Text;

namespace BAI_GIU_XE
{
    public enum barrierStatus { OpenBarrier, CloseBarrier }
    public class BarrierEventArgs : EventArgs
    {
        private barrierStatus _data;
        public barrierStatus data { set { } get { return _data; } }
        public BarrierEventArgs(barrierStatus t) : base()
        {
            this._data = t;
        }
    }
    public class Barrier
    {
        public string vitri { set; get; }
        public barrierStatus status { set; get; }
        public Barrier() {
            this.status = barrierStatus.CloseBarrier;
        }
        public Barrier (string vitri)
        {
            this.status = barrierStatus.CloseBarrier;
            this.vitri = vitri;
        }
        public Barrier (Barrier x)
        {
            this.status = x.status;
            this.vitri = x.vitri;
        }
        public void BarrierAct(Xe a, MyEventArgs e)
        {
            this.status = e.data;
        }
    }
}
