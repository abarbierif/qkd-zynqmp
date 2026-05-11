`timescale 1ns / 1ps
module pmodADC(
input sysclk,clk_DAC_E,PinADC,
output reg[11:0]dataADC=0,
output reg SyncADC=0
     );
//pmodADC adcc01(.sysclk(sysclk),.clk_DAC_E(clk_DAC_E),.dataADC(dataADC),.SyncADC(SyncADC),.PinADC( PinADC));     
  
reg[4:0] index=0;   
reg[11:0]D=0;

always@(posedge  sysclk) begin

if(clk_DAC_E)
case(index)
0: begin SyncADC<=1;   index<=1;   end
1: begin SyncADC<=0;  index<=2;   end
2: begin SyncADC<=0;  index<=3;   end
3: begin SyncADC<=0;  index<=4;   end
4: begin SyncADC<=0;  index<=5;   end
5: begin SyncADC<=0;  index<=6;  D[11]<=PinADC; end
6: begin SyncADC<=0;  index<=7;  D[10]<=PinADC;  end
7: begin SyncADC<=0;  index<=8;  D[09]<=PinADC;  end
8: begin SyncADC<=0;  index<=9;  D[8]<=PinADC;end
9: begin SyncADC<=0;  index<=10;  D[7]<=PinADC; end
10: begin SyncADC<=0;  index<=11; D[6]<=PinADC; end
11: begin SyncADC<=0;  index<=12; D[5]<=PinADC; end
12: begin SyncADC<=0;  index<=13; D[4]<=PinADC;  end
13: begin SyncADC<=0;  index<=14;  D[3]<=PinADC;end
14: begin SyncADC<=0;  index<=15;  D[2]<=PinADC; end
15: begin SyncADC<=0;  index<=16;  D[4]<=PinADC;end
16: begin SyncADC<=0;  index<=17; D[0]<=PinADC; end
17: begin SyncADC<=0;  index<=0;  dataADC<=D;end
endcase
end
endmodule
