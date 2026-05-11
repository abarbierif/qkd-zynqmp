`timescale 1ns / 1ps

module top(
input sysclk,RsRx,
output RsTx,jc_PMF,
input [3:0] sw,
output [3:0]led,
output [7:0]je,
output [3:0]jd,
input[3:0]jbi,
output[3:0]jbo
//output [1:0] jdo,
//input [1:0]jdi 
    );
 wire [32:0]Pmuestreo;   
    SyncMaestro sync01(.sysclk(sysclk),.Pmuestreo(Pmuestreo),.Sync(SyncMaster)  );
//assign led=sw;
wire[15:0] amplificador;
//wire [32:0]dataADCtx;
wire [48:0] dataADC;
wire[11:0]Amplificar;
ProcesaADC PADC01(.sysclk(sysclk),.jbi(jbi),.jbo(jbo),.amplificador(amplificador)
,.dataADC(dataADC),.SyncMaster(SyncMaster),.Amplificar(Amplificar) );
///////////////////////////////////////////////////////////////////////////////////////////////////////////
wire [48:0] dataDaCTx;
wire [36:0] dataDaC_F;
wire [36:0] dataDaC_C;
wire[35:0]dataF_Rx;
ProcesaDACs Pdac01(.sysclk(sysclk),.je(je),.jd(jd),.dataDaC_F(dataDaC_F),
.dataDaC_C(dataDaC_C),.odataDaCTx(dataDaCTx),.SyncMaster(SyncMaster) );
///////////////////////////////////////////////////////////////////////////////////////////////////////////
wire[31:0]pAlgControl,Tphase,TcontrolStable;
Mod_Control mctrl01(.sysclk(sysclk),.dataADC(dataADC),.dataF_Rx(dataF_Rx),.CtrlOk(CtrlOk),.sync(SyncMaster),.dataDaC_F(dataDaC_F),.dataDaC_C(dataDaC_C),
.pAlgControl(pAlgControl),.Tphase(Tphase),.TcontrolStable(TcontrolStable),.jc_PMF(jc_PMF));
///////////////////////////////////////////////////////////////////////////////////////////////////////////
wire[32:0]dataADCtx;
assign dataADCtx[32]=dataADC[48];
assign dataADCtx[31:24]=dataADC[47:40];
assign dataADCtx[23:16]=dataADC[35:28];
assign dataADCtx[15:08]=dataADC[23:16];
assign dataADCtx[07:00]=dataADC[11:04];

ControlProceso CPr00(.sysclk(sysclk),.RsRx(RsRx),.RsTx(RsTx),.pAlgControl(pAlgControl),.Tick()
,.amplificador(amplificador), .dataADCtx(dataADCtx),.dataDaCTx(dataDaCTx), .Pmuestreo(Pmuestreo),
.dataF_Rx(dataF_Rx),.Amplificar(Amplificar),.Tphase(Tphase),.TcontrolStable(TcontrolStable));

assign led[0]=~RsRx;
assign led[1]=~RsTx;
assign led[2]=CtrlOk;
assign led[3]=pAlgControl[31];
endmodule
