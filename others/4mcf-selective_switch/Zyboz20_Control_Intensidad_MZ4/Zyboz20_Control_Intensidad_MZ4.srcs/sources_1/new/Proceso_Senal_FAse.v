`timescale 1ns / 1ps

module Proceso_Senal_FAse(
input sysclk,sync,EnFaseC,
input[35:0]dataF_Rx,

input[2:0]En_PM,
output [35:0]dataDaC_F
   );
   
reg[11:0]dataDac_F1=0;
reg[11:0]dataDac_F2=0;
reg[11:0]dataDac_F3=0;   

assign dataDaC_F={dataDac_F3,dataDac_F2,dataDac_F1} ; 
  
OneShot osN(.sysclk(sysclk), .signal(sync),.Os(osync)); 
reg[3:0]index=0;
reg[3:0]count=0;
always@(posedge sysclk)begin
if(~EnFaseC)begin
        dataDac_F1<=0;
        dataDac_F2<=0;
        dataDac_F3<=0;
        index<=0; 
end
else begin
    case(index)
    
        0:begin
             if(En_PM[0])dataDac_F1<=dataF_Rx[11:0];   
             if(En_PM[1])dataDac_F2<=dataF_Rx[23:12];
             if(En_PM[2])dataDac_F3<=dataF_Rx[35:24];
             index<=1;  
        end
        1:begin
            if(osync)  count<=count+1;    
              if(&count)  begin
                    index<=2;
                    dataDac_F1<=0;
                    dataDac_F2<=0;
                    dataDac_F3<=0;
               end   
        end
        2:begin
            if(osync)  count<=count+1;    
              if(&count)  begin
                    if(En_PM[0])dataDac_F1<=dataF_Rx[11:0];   
                     if(En_PM[1])dataDac_F2<=dataF_Rx[23:12];
                     if(En_PM[2])dataDac_F3<=dataF_Rx[35:24];
                     index<=1;  
              end   
        end
    endcase
end
end
endmodule
