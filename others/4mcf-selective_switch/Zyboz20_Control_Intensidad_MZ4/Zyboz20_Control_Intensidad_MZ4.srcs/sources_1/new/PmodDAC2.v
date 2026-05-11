`timescale 1ns / 1ps
module PmodDAC2(

input sysclk,clk_DAC_E,EnDAC,
input [11:0]dataDac1,dataDac2,
output reg SyncDac=0,PinDAC1=0,PinDAC2=0
     );
//PmodDAC2 dac00(.sysclk(sysclk),.clk_DAC_E(clk_DAC_E),.EnDAC(EnDAC),.dataDac1(dataDac1),.dataDac2(dataDac2)
//,.SyncDac(SyncDac),.PinDAC1(PinDAC1),.PinDAC2(PinDAC2) );
  
//pmodDAC dac01(.sysclk(sysclk),.clk_DAC_E(clk_DAC_E),.dataDac(dataDac),.SyncDac(SyncDac),.PinDAC(PinDAC));     
  
reg[4:0] index=0;   
reg[11:0]D1=0,D2=0;

always@(posedge  sysclk) begin

if(clk_DAC_E & EnDAC)
case(index)
00: begin SyncDac<=1;  index<=01;  PinDAC1<=0;    PinDAC2<=0;       D1<=dataDac1; D2<=dataDac2; end
01: begin SyncDac<=0;  index<=02;  PinDAC1<=0;    PinDAC2<=0;       end
02: begin SyncDac<=0;  index<=03;  PinDAC1<=0;    PinDAC2<=0;       end
03: begin SyncDac<=0;  index<=04;  PinDAC1<=0;    PinDAC2<=0;       end
04: begin SyncDac<=0;  index<=05;  PinDAC1<=0;    PinDAC2<=0;       end
05: begin SyncDac<=0;  index<=06;  PinDAC1<=D1[11];PinDAC2<=D2[11];   end
06: begin SyncDac<=0;  index<=07;  PinDAC1<=D1[10];PinDAC2<=D2[10];   end
07: begin SyncDac<=0;  index<=08;  PinDAC1<=D1[9]; PinDAC2<=D2[9];    end
08: begin SyncDac<=0;  index<=09;  PinDAC1<=D1[8]; PinDAC2<=D2[8];    end
09: begin SyncDac<=0;  index<=10;  PinDAC1<=D1[7]; PinDAC2<=D2[7];    end
10: begin SyncDac<=0;  index<=11;  PinDAC1<=D1[6]; PinDAC2<=D2[6];    end
11: begin SyncDac<=0;  index<=12;  PinDAC1<=D1[5]; PinDAC2<=D2[5];    end
12: begin SyncDac<=0;  index<=13;  PinDAC1<=D1[4]; PinDAC2<=D2[4];    end
13: begin SyncDac<=0;  index<=14;  PinDAC1<=D1[3]; PinDAC2<=D2[3];    end
14: begin SyncDac<=0;  index<=15;  PinDAC1<=D1[2]; PinDAC2<=D2[2];    end
15: begin SyncDac<=0;  index<=16;  PinDAC1<=D1[1]; PinDAC2<=D2[1];    end
16: begin SyncDac<=0;  index<=17;  PinDAC1<=D1[0]; PinDAC2<=D2[0];    end
17: begin SyncDac<=1;  index<=00;  PinDAC1<=0;    PinDAC2<=0;       end
endcase


end
     
endmodule

//input sysclk,clk_DAC_E,EnDAC,
//input [11:0]dataDac1,dataDac2,
//output reg SyncDac=0,PinDAC1=0,PinDAC2=0
//     );
////reg[11:0]dataDac1=0,dataDac2=0;
////PmodDAC2 dac00(.sysclk(sysclk),.clk_DAC_E(clk_DAC_E),.EnDAC(EnDAC),.dataDac1(dataDac1),.dataDac2(dataDac2),.SyncDac(SyncDac),.PinDAC1(PinDAC1),.PinDAC2(PinDAC1) );
     
//reg[1:0] index=0;   
//reg[11:0]D1=0;
//reg[11:0]D2=0;
//reg[3:0]count=0;
//always@(posedge  sysclk) begin
 
//if(clk_DAC_E )
//case(index)
//0: begin SyncDac<=1;   if(EnDAC) index<=1;  PinDAC1<=0; PinDAC2<=0;  D1<=dataDac1; D2<=dataDac2; count<=0;end
//1: begin
//SyncDac<=0; 
//    if(count==2)begin
//        index<=2;
//        count<=0;
//    end
//    else
//      count<=count+1; 
    
//end 

//2: begin 
//    if(count==11)begin
//        index<=3;
//        count<=0;
//    end
//    else
//      count<=count+1; 
      
//      PinDAC1<=D1[11-count];
//      PinDAC2<=D2[11-count];
//end
//3: begin SyncDac<=1;  index<=0;  PinDAC1<=0; PinDAC2<=0;  end
//endcase
 


//end
//endmodule
