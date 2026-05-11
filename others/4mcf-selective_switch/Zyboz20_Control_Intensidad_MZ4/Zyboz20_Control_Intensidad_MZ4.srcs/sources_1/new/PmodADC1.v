`timescale 1ns / 1ps
module PmodADC1(
input sysclk,clk_ADC_E,EnADC,PinADC1,PinADC2,
output reg[11:0]dataADC1,dataADC2,
output reg SyncADC=0
     );

reg[4:0] index=0;   
reg[11:0]D1=0,D2=0;

always@(posedge  sysclk) begin

if(clk_ADC_E & EnADC)
case(index)
00: begin SyncADC<=1;  index<=01;       end    // SyncADC corresponde al Chip Select, en este momento no lee data   
01: begin SyncADC<=0;  index<=02;        end   // Aquí realiza el cambio para comenzar a leer la data con 4 ceros iniciales definido por fabricante
02: begin SyncADC<=0;  index<=03;        end
03: begin SyncADC<=0;  index<=04;        end
04: begin SyncADC<=0;  index<=05;        end
05: begin SyncADC<=0;  index<=06;  D1[11]<=PinADC1; D2[11]<=PinADC2;   end // Luego de los 4 ceros la data se comienza a guardar, los datos son de 12 bit
06: begin SyncADC<=0;  index<=07;  D1[10]<=PinADC1; D2[10]<=PinADC2;   end
07: begin SyncADC<=0;  index<=08;  D1[09]<=PinADC1; D2[09]<=PinADC2;    end
08: begin SyncADC<=0;  index<=09;  D1[08]<=PinADC1; D2[08]<=PinADC2;    end
09: begin SyncADC<=0;  index<=10;  D1[07]<=PinADC1; D2[07]<=PinADC2;    end
10: begin SyncADC<=0;  index<=11;  D1[06]<=PinADC1; D2[06]<=PinADC2;    end
11: begin SyncADC<=0;  index<=12;  D1[05]<=PinADC1; D2[05]<=PinADC2;    end
12: begin SyncADC<=0;  index<=13;  D1[04]<=PinADC1; D2[04]<=PinADC2;    end
13: begin SyncADC<=0;  index<=14;  D1[03]<=PinADC1; D2[03]<=PinADC2;    end
14: begin SyncADC<=0;  index<=15;  D1[02]<=PinADC1; D2[02]<=PinADC2;    end
15: begin SyncADC<=0;  index<=16;  D1[01]<=PinADC1; D2[01]<=PinADC2;    end
16: begin SyncADC<=0;  index<=17;  D1[00]<=PinADC1; D2[00]<=PinADC2;    end
17: begin SyncADC<=1;  index<=00;  dataADC1<=D1; dataADC2<=D2;        end     // Aquí el Chip Select deja de guardar la información con 2 siclos en 1, esto es debido a que el tiempo minimo a 20[MHz] es de 50ns, como sobrepasa un poco los 20, se ocupan 2 siclos.
endcase


end
     
endmodule