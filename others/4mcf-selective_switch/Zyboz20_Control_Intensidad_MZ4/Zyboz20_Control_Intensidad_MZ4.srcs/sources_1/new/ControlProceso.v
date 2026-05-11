`timescale 1ns / 1ps
module ControlProceso(
input sysclk,RsRx,
output  RsTx,Tick,
output[15:0] amplificador,
output [31:0]pAlgControl,Pmuestreo,Tphase,TcontrolStable,
output[35:0]dataF_Rx,
input [32:0]dataADCtx,
input [48:0] dataDaCTx,
 output   [11:0] Amplificar
    );
//ControlProceso CPr00(.sysclk(sysclk),.RsRx(RsRx),.RsTx(RsTx),.pAlgControl(pAlgControl));
wire[11:0]trigger;
wire[31:0]ordenTx;
UART_clock TickGen ( .clk_100Mhz(sysclk),   .Tick(Tick)  );   
Control_Rx CRx01 (
    .clk_100Mhz( sysclk), 
    .Tick(Tick), 
    .RsRx(RsRx),
    .pAlgControl(pAlgControl),
    .amplificador(amplificador),
    .Pmuestreo(Pmuestreo),
    .dataF_Rx(dataF_Rx),
    .ordenTx(ordenTx),
    .Amplificar(Amplificar),
    .Tphase(Tphase),
    .trigger(trigger),
    .TcontrolStable(TcontrolStable)
    );
 Control_Tx CTx01 (
    .clk_100Mhz(sysclk), 
    .Tick(Tick), 
    .RsTx(RsTx), 
    .dataADCtx(dataADCtx),
    .dataDaCTx(dataDaCTx),
    .ordenTx(ordenTx),
     .trigger(trigger));
     
    
endmodule
