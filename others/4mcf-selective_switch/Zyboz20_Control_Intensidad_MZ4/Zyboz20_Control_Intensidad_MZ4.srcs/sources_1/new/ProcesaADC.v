`timescale 1ns / 1ps

module ProcesaADC(
input sysclk,SyncMaster,
input[3:0]jbi,
output[3:0]jbo,
input[15:0] amplificador,
output reg [48:0]dataADC=0, 
input   [11:0] Amplificar
    );
///////////////////////////////////////////////////////////////////////////////////////////////////////////
wire[11:0]dataADC1,dataADC2,dataADC3,dataADC4;
 ModADCs m0(.sysclk(sysclk),.EnADC(1),.dataADC1(dataADC1),.dataADC2(dataADC2),.dataADC3(dataADC3),.dataADC4(dataADC4),
.readyADC(readyADC),.jbi(jbi),.jbo(jbo));
OneShot osN(.sysclk(sysclk), .signal(SyncMaster),.Os(oSyncMaster)); 
reg[11:0] count=0;
reg[4:0]pausa=0;
reg indexP=0;
always@(posedge sysclk)begin
 
count[11:0]<=count[11:0]+1;
if(oSyncMaster) begin
dataADC[11:00]<=dataADC1[11:0]*Amplificar[11:0];   	
dataADC[23:12]<=dataADC2[11:0]*Amplificar[11:0];   
dataADC[35:24]<=dataADC3[11:0]*Amplificar[11:0]; 	
dataADC[47:36]<=dataADC4[11:0]*Amplificar[11:0];  

//lo comentado es una señal de prueba 
//dataADC[11:0]<=dataADC1[11:0];   	
//dataADC[23:12]<=dataADC2[11:0];   
//dataADC[35:24]<=dataADC3[11:0]; 	
//dataADC[47:36]<=dataADC4[11:0];   
//dataADC[11:0]<=count[11:0];   	
//dataADC[23:12]<=count[11:0]+500;   
//dataADC[35:24]<=count[11:0]+1000; 	
//dataADC[47:36]<=count[11:0]+1500; 
end
case(indexP)
0:begin   if(oSyncMaster) begin dataADC[48]<=1; indexP<=1; end end
1:begin if(&pausa)begin dataADC[48]<=0; indexP<=0; end else pausa<=pausa+1; end
endcase
  
//dataADCtx[32]<=readyADC;
end

endmodule
