`timescale 1ns / 1ps
module ProcesaDACs(
input sysclk,SyncMaster,
output [7:0]je,
output [3:0]jd,
input [36:0] dataDaC_F,
input [36:0] dataDaC_C,
output  reg[48:0] odataDaCTx=0 
//output reg[48:0] odataDaCTx=0
    );
//ProcesaDACs Pdac01(.sysclk(sysclk),.je(je),.jd(jd),.dataDaC_F(dataDaC_F),.dataDaC_C(dataDaC_C),.odataDaCTx(odataDaCTx) );

wire[11:0]dataDac_C1=dataDaC_C[11:00],dataDac_F1=dataDaC_F[11:00];
wire[11:0]dataDac_C2=dataDaC_C[23:12],dataDac_F2=dataDaC_F[23:12];
wire[11:0]dataDac_C3=dataDaC_C[35:24],dataDac_F3=dataDaC_F[35:24];
ModDACs ModDac00(.sysclk(sysclk),.EnDAC(1),.dataDac_C1(dataDac_C1),.dataDac_F1(dataDac_F1),.dataDac_C2(dataDac_C2),
.dataDac_F2(dataDac_F2),.dataDac_C3(dataDac_C3),.dataDac_F3(dataDac_F3),.readyDac(readyDac),.je(je),.jd(jd));
///////////////////////////////////////////////////////////////////////////////////////////////////////////
//assign odataDaCTx[48:0]={SyncMaster,dataDac_C3[11:4],dataDac_C2[11:4],dataDac_C1[11:4],
//                                dataDac_F3[11:4],dataDac_F2[11:4],dataDac_F1[11:4]};
OneShot osN(.sysclk(sysclk), .signal(SyncMaster),.Os(oSyncMaster)); 
always@(posedge sysclk)begin
if(oSyncMaster) begin
odataDaCTx[07:00]<=dataDac_F1[11:4];   	 //aqui se guardan registros para saber que esta haciendo el DAC
odataDaCTx[15:08]<=dataDac_F2[11:4];   
odataDaCTx[23:16]<=dataDac_F3[11:4]; 	
odataDaCTx[31:24]<=dataDac_C1[11:4];  
odataDaCTx[39:32]<=dataDac_C2[11:4];  
odataDaCTx[47:40]<=dataDac_C3[11:4];  
end
odataDaCTx[48]<=SyncMaster;
end
      
//reg[7:0] count=0;
//OneShot osN(.sysclk(sysclk), .signal(readyDac),.Os(oreadyDac)); 
//always@(posedge sysclk)begin
//if(oreadyDac)
//count[7:0]<=count[7:0]+1;

//odataDaCTx[07:00]<=count[7:0]+8'd00;   	
//odataDaCTx[15:08]<=count[7:0]+8'd20;   
//odataDaCTx[23:16]<=count[7:0]+8'd40;

//odataDaCTx[31:24]<=255-count[7:0];   
//odataDaCTx[39:32]<=235-count[7:0];   
//odataDaCTx[47:40]<=215-count[7:0];   
//odataDaCTx[48]<=readyDac;
//end                         
endmodule
