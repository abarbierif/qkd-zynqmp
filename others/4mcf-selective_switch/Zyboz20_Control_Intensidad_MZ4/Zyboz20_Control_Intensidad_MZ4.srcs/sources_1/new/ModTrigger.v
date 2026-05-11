`timescale 1ns / 1ps
module ModTrigger(
input sysclk, resetFifo,
input[11:0]SignalTriguer, //8+4=12bits
input [32:0]dataADCtx,
input [48:0] dataDaCTx,
output reg EnW=0
   );
//   ModTrigger trg01(.sysclk(sysclk), .resetFifo(resetFifo),.SignalTriguer(SignalTriguer),.dataADCtx(dataADCtx),.dataDaCTx(dataDaCTx),.EnW(EnW) );
reg[3:0] index=0;
wire[3:0] ctrlIndex=SignalTriguer[11:8];
wire[7:0] UmbralIndex=SignalTriguer[7:0];
always@(posedge sysclk)begin
case(index)
0:begin 
   EnW<=1; 
   index<=11;
end
1:begin 
   if(((dataADCtx[7:0]-10)>UmbralIndex) & ((dataADCtx[7:0]+10)<UmbralIndex))begin
   EnW<=1; 
   index<=11;
   end
end
2:begin 
   if(((dataADCtx[15:8]-10)>UmbralIndex) & ((dataADCtx[15:8]+10)<UmbralIndex))begin
   EnW<=1; 
   index<=11;
   end
end
3:begin 
   if(dataADCtx[23:16]>UmbralIndex)begin
   EnW<=1; 
   index<=11;
   end
end
4:begin 
   if(dataADCtx[31:24]>UmbralIndex)begin
   EnW<=1; 
   index<=11;
   end
end
5:begin 
   if(dataDaCTx[7:0]>UmbralIndex)begin
   EnW<=1; 
   index<=11;
   end
end
6:begin 
   if(dataDaCTx[15:8]>UmbralIndex)begin
   EnW<=1; 
   index<=11;
   end
end
7:begin 
   if(dataDaCTx[23:16]>UmbralIndex)begin
   EnW<=1; 
   index<=11;
   end
end
8:begin 
   if(dataDaCTx[31:24]>UmbralIndex)begin
   EnW<=1; 
   index<=11;
   end
end
9:begin 
   if(dataDaCTx[39:32]>UmbralIndex)begin
   EnW<=1; 
   index<=11;
   end
end
10:begin 
   if(dataDaCTx[47:40]>UmbralIndex)begin
   EnW<=1; 
   index<=11;
   end
end
11:begin 
   if(resetFifo)begin EnW<=0; index<=12; end
end
12:begin 
   index<=ctrlIndex;
end
endcase
end
endmodule
