`timescale 1ns / 1ps
module Mod_Control(
input sysclk,sync,
input [48:0]dataADC,
input [35:0] dataF_Rx,
output CtrlOk,
output reg jc_PMF=0,
output [36:0] dataDaC_F,
output [36:0] dataDaC_C,
input [31:0]pAlgControl,Tphase,TcontrolStable
    );
wire[2:0]selector=pAlgControl[30:28];
 wire[11:0]I_Actual,I1,I2;
 assign I1=(selector[0])?dataADC[23:12]:dataADC[11:0];
  assign I2=(selector[0])?dataADC[47:36]:dataADC[35:24];
 assign I_Actual=(selector[1])?I2:I1;  
 
//Selector_Intensidad SI01(.sysclk(sysclk),.selector(selector),.dataADC(dataADC),.I_Actual(I_Actual)  );   
// wire [7:0]ordenControl=pAlgControl[7:0];
wire [4:0]ordenControl=pAlgControl[4:0];
wire [2:0]En_PM=pAlgControl[7:5];
wire[11:0]pasos;
assign pasos[7:0]=pAlgControl[15:8];
wire[11:0]ref=pAlgControl[27:16];
 reg EnControl=1;
Proceso_Senal_Control PSC01(.sysclk(sysclk),.sync(sync),.EnControl(EnControl),
.ordenControl(ordenControl),.pasos(pasos),.I_Actual(I_Actual),.ref(ref),
.CtrlOk(CtrlOk),.dataDaC_C(dataDaC_C),.En_PM(En_PM) );

wire EnFase=pAlgControl[31];
//reg EnFaseC=0;
//Proceso_Senal_FAse PSF01(.sysclk(sysclk), .sync(sync),.EnFaseC(EnFaseC),
//.dataF_Rx(dataF_Rx), .dataDaC_F(dataDaC_F),.En_PM(En_PM)  );

reg[3:0]index=0;
//reg[31:0]Tphase=255;
reg[31:0]count=0;
OneShot osN(.sysclk(sysclk), .signal(sync),.Os(osync)); 
reg[11:0]dataDac_F1=0;
reg[11:0]dataDac_F2=0;
reg[11:0]dataDac_F3=0;   

assign dataDaC_F={dataDac_F3,dataDac_F2,dataDac_F1} ; 
always@(posedge sysclk)begin
if(~EnFase)begin
        dataDac_F1<=0;
        dataDac_F2<=0;
        dataDac_F3<=0;
     EnControl<=1; 
     index<=0;
end 
else begin
case(index)
0:begin 
  if(osync)begin  if(CtrlOk)begin EnControl<=0;  index<=1;end else EnControl<=1; end
 end
1:begin 
//     if(osync)
     count<=count+1;
     if(count>=Tphase)begin 
     jc_PMF<=1;
             if(En_PM[0])dataDac_F1<=0;   
             if(En_PM[1])dataDac_F2<=dataF_Rx[23:12];
             if(En_PM[2])dataDac_F3<=dataF_Rx[35:24];
     count<=0;
     index<=2;
     end
 end
 2:begin 
//     if(osync)
     count<=count+1;
     if(count>=Tphase)begin 
      jc_PMF<=0;
             if(En_PM[0])dataDac_F1<=dataF_Rx[11:0];   
             if(En_PM[1])dataDac_F2<=0;
             if(En_PM[2])dataDac_F3<=dataF_Rx[35:24];
     count<=0;
     index<=3;
     end
 end
 3:begin 
//     if(osync)
     count<=count+1;
     if(count>=Tphase)begin 
      jc_PMF<=1;
             if(En_PM[0])dataDac_F1<=dataF_Rx[11:0];   
             if(En_PM[1])dataDac_F2<=dataF_Rx[23:12];
             if(En_PM[2])dataDac_F3<=0;
     count<=0;
     index<=4;
     end
 end
 4:begin 
//   if(osync)
   count<=count+1;
     if(count>=Tphase)begin 
        dataDac_F1<=0;
        dataDac_F2<=0;
        dataDac_F3<=0;
         jc_PMF<=0;
     count<=0;
     index<=6;
     EnControl<=1; 
     end
 end
// 5:begin 
  
////   if(osync)
//   count<=count+1;
//     if(count>=Tphase)begin 
//     count<=0;
//     index<=6;
//     end
// end
 6:begin 
//     if(osync)
     count<=count+1;
     if(count>=Tphase)begin 
             if(En_PM[0])dataDac_F1<=0;   
             if(En_PM[1])dataDac_F2<=dataF_Rx[23:12];
             if(En_PM[2])dataDac_F3<=dataF_Rx[35:24];
     count<=0;
     index<=7;
     end
 end
 7:begin 
  count<=count+1;
     if(count>=Tphase)begin 
             if(En_PM[0])dataDac_F1<=0;   
             if(En_PM[1])dataDac_F2<=0;
             if(En_PM[2])dataDac_F3<=0;
     count<=0;
     index<=8;
     end
     
//    if(osync)begin EnControl<=1;   index<=0;end  
 end
 8:begin 
   if(osync)count<=count+1;
     if(count>=TcontrolStable)begin 
     EnControl<=1;      
     count<=0;
     index<=0;
     end
    end
    
 
endcase
end
end
   
endmodule
