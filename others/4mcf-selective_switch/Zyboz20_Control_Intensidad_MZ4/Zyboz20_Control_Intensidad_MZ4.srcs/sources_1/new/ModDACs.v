`timescale 1ns / 1ps

module ModDACs(
input sysclk,EnDAC,
input[11:0]dataDac_C1,dataDac_F1,dataDac_C2,dataDac_F2,dataDac_C3,dataDac_F3,
output readyDac,
output [7:0]je,
output [3:0]jd
    );
//divClock div01(.sysclk(sysclk),.clk1(clk_DAC_ADC_E),.umbral(32'd5));
divClockS divS01(.sysclk(sysclk),.clk1(clk_DAC_ADC_S),.umbral(32'd5));//SIMÉTRICO
OneShot osN(.sysclk(sysclk), .signal(~clk_DAC_ADC_S),.Os(clk_DAC_ADC_E)); // el clk_DAC_ADC_S está negado para que trabaje en el flanco de bajada
///////////////////////////////////////////////////////////////////////////////////////////////////////////
PmodDAC2 dac01(.sysclk(sysclk),.clk_DAC_E(clk_DAC_ADC_E),.EnDAC(EnDAC),.dataDac1(dataDac_C1)
              ,.dataDac2(dataDac_F1),.SyncDac(SyncDac1),.PinDAC1(PinDAC_C1),.PinDAC2(PinDAC_F1) );
assign jd[0]=SyncDac1;
assign jd[1]=PinDAC_F1;
assign jd[2]=PinDAC_C1;
assign jd[3]=clk_DAC_ADC_S;
///////////////////////////////////////////////////////////////////////////////////////////////////////////
PmodDAC2 dac02(.sysclk(sysclk),.clk_DAC_E(clk_DAC_ADC_E),.EnDAC(EnDAC),.dataDac1(dataDac_C2)
              ,.dataDac2(dataDac_F2),.SyncDac(SyncDac2),.PinDAC1(PinDAC_C2),.PinDAC2(PinDAC_F2) );
assign je[0]=SyncDac2;
assign je[1]=PinDAC_F2;
assign je[2]=PinDAC_C2;
assign je[3]=clk_DAC_ADC_S;
///////////////////////////////////////////////////////////////////////////////////////////////////////////
PmodDAC2 dac03(.sysclk(sysclk),.clk_DAC_E(clk_DAC_ADC_E),.EnDAC(EnDAC),.dataDac1(dataDac_C3)
              ,.dataDac2(dataDac_F3),.SyncDac(SyncDac3),.PinDAC1(PinDAC_C3),.PinDAC2(PinDAC_F3) );
assign je[4]=SyncDac3;
assign je[5]=PinDAC_F3;
assign je[6]=PinDAC_C3;
assign je[7]=clk_DAC_ADC_S;
///////////////////////////////////////////////////////////////////////////////////////////////////////////
assign readyDac=SyncDac3;

endmodule
