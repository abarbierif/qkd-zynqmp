`timescale 1ns / 1ps
module SimulaADCs(
input sysclk,EnADC,
output reg[11:0] dataADC1,dataADC2,dataADC3,dataADC4
    );
  //SimulaADCs sda1(.sysclk(sysclk),.EnADC(1),.dataADC1(dataADC1),
//  .dataADC2(dataADC2),.dataADC3(dataADC3),.dataADC4(dataADC4));  
divClock div01(.sysclk(sysclk),.clk1(SyncMaster),.umbral(32'd100));
 OneShot osN(.sysclk(sysclk), .signal(SyncMaster),.Os(oSyncMaster)); 
reg signo1=1;
reg signo2=1;
reg signo3=1;
reg signo4=1;
always@(posedge sysclk)begin
    if(oSyncMaster &EnADC) begin
if(signo1)      dataADC1[10:0]<=dataADC1[10:0]+1; 
else   	dataADC1[10:0]<=dataADC1[10:0]-1; 
if(dataADC1[10:0]<10)signo1<=1;
if(dataADC1[10:0]>500)signo1<=0;

    dataADC2<=dataADC1+500;   
    dataADC3<=dataADC1+1000; 	
    dataADC4<=dataADC1+1500;   
    end
end
endmodule
