`timescale 1ns / 1ps
module pmodDAC(
input sysclk,clk_DAC_E,
input [11:0]dataDac,
output reg SyncDac=0,PinDAC=0
     );
//pmodDAC dac01(.sysclk(sysclk),.clk_DAC_E(clk_DAC_E),.dataDac(dataDac),.SyncDac(SyncDac),.PinDAC(PinDAC));     
  
reg[4:0] index=0;   
reg[11:0]D=0;

always@(posedge  sysclk) begin

if(clk_DAC_E)
case(index)
0: begin SyncDac<=1;   index<=1; PinDAC<=0; D<=dataDac; end
1: begin SyncDac<=0;  index<=2;  PinDAC<=0; end
2: begin SyncDac<=0;  index<=3;  PinDAC<=0; end
3: begin SyncDac<=0;  index<=4;  PinDAC<=0; end
4: begin SyncDac<=0;  index<=5;  PinDAC<=0; end
5: begin SyncDac<=0;  index<=6;  PinDAC<=D[11]; end
6: begin SyncDac<=0;  index<=7;  PinDAC<=D[10]; end
7: begin SyncDac<=0;  index<=8;  PinDAC<=D[9]; end
8: begin SyncDac<=0;  index<=9;  PinDAC<=D[8]; end
9: begin SyncDac<=0;  index<=10;  PinDAC<=D[7]; end
10: begin SyncDac<=0;  index<=11;  PinDAC<=D[6]; end
11: begin SyncDac<=0;  index<=12;  PinDAC<=D[5]; end
12: begin SyncDac<=0;  index<=13;  PinDAC<=D[4]; end
13: begin SyncDac<=0;  index<=14;  PinDAC<=D[3]; end
14: begin SyncDac<=0;  index<=15;  PinDAC<=D[2]; end
15: begin SyncDac<=0;  index<=16;  PinDAC<=D[1]; end
16: begin SyncDac<=0;  index<=17;  PinDAC<=D[0]; end
17: begin SyncDac<=0;  index<=0;  PinDAC<=0; end
endcase


end
     
endmodule
