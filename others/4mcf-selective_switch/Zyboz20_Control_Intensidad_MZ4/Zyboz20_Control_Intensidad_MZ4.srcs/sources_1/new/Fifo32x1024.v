`timescale 1ns / 1ps
module Fifo32x1024(
input sysclk,ReadFifo,reset,
input [32:0]dataADCtx,
output [31:0]odataADC,
output full,empty
    );
//    Fifo32x1024 f01(.sysclk(sysclk),.ReadFifo(ReadFifo),.reset(reset),.dataADCtx(dataADCtx)
//    ,.odataADC(odataADC),.full(full),.empty(empty));
    
    
wire[31:0] inmen=dataADCtx[31:0];
//wire Ew=dataADCtx[0];
OneShot os1(.sysclk(sysclk), .signal(dataADCtx[32]),.Os(Ew));
 reg[10:0] indexW=0, indexR=0;
 reg[32:0] FiFomem[2**10-1:0]; 
 reg [31:0]outmen=0;
 assign  odataADC=outmen;
 assign empty=(indexW==indexR)? 1:0;
  assign full=((indexW[10]!=indexR[10])&(indexW[9:0]==indexR[9:0]))? 1:0;
always@(posedge sysclk) begin
     if(reset) begin indexW[10:0]<= 0; indexR[10:0]<=0; end
     else begin
         if(Ew && ~full)begin
             FiFomem[ indexW[9:0] ]<=inmen;
             indexW[10:0]<= indexW[10:0]+11'd1;
         end 
         if(ReadFifo && ~empty)begin
            outmen<= FiFomem[ indexR[9:0] ];
             indexR[10:0]<= indexR[10:0]+11'd1;
         end 
     end
end  
endmodule
