`timescale 1ns / 1ps

module ModADCs(
input sysclk,EnADC,
output[11:0]dataADC1,dataADC2,dataADC3,dataADC4,
//input[31:0]muestreoADC,
output readyADC, //esto no se utilizó en el código
input[3:0]jbi,
output[3:0]jbo
    );
 
// ModADCs(.sysclk(sysclk),.EnADC(EnADC),.dataADC1(dataADC1),.dataADC2(dataADC2),.dataADC3(dataADC3),.dataADC4(dataADC4),
//.readyADC(readyADC),.jbi(jbi),.jbo(jbo));
////////////////////////////////////////////////////////////////////////////////
//divClock div01(.sysclk(sysclk),.clk1(clk_ADC_E),.umbral(32'd5));
divClockS divS01(.sysclk(sysclk),.clk1(clk_ADC_S),.umbral(32'd5));//SIMÉTRICO 
OneShot osN(.sysclk(sysclk), .signal(clk_ADC_S),.Os(clk_ADC_E)); //se define un oneshot para activar el ADC
////////////////////////////////////////////////////////////////////////////////
PmodADC1 ADC01(.sysclk(sysclk),.clk_ADC_E(clk_ADC_E),.EnADC(EnADC),.dataADC1(dataADC1)
,.dataADC2(dataADC2),.SyncADC(SyncADC1),.PinADC1(PinADC1),.PinADC2(PinADC2) );
assign jbo[0]=SyncADC1;
assign PinADC1=jbi[0];
assign PinADC2=jbi[1];
assign jbo[1]=clk_ADC_S;
////////////////////////////////////////////////////////////////////////////////
PmodADC1 ADC03(.sysclk(sysclk),.clk_ADC_E(clk_ADC_E),.EnADC(EnADC),.dataADC1(dataADC3)
,.dataADC2(dataADC4),.SyncADC(SyncADC2),.PinADC1(PinADC3),.PinADC2(PinADC4) );

//PmodADC1 ADC02(.sysclk(sysclk),.clk_ADC_E(clk_ADC_E),.EnADC(EnADC),.dataADC1(dataADC3)
//,.dataADC2(dataADC4),.SyncADC(SyncADC2),.PinADC1(PinADC3),.PinADC2(PinADC4) );
assign jbo[2]=SyncADC2;
assign PinADC3=jbi[2];
assign PinADC4=jbi[3];
assign jbo[3]=clk_ADC_S;
//assign jbo[2]=0;
//assign PinADC3=jbi[2];
//assign PinADC4=jbi[3];
//assign jbo[3]=0;
////////////////////////////////////////////////////////////////////////////////
assign readyADC=SyncADC2;
//OneShot osN(.sysclk(sysclk), .signal(SyncADC2),.Os(readyADC)); 
endmodule
