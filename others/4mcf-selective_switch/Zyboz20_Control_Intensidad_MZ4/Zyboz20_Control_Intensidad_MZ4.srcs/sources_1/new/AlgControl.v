`timescale 1ns / 1ps
module AlgControl(
input sysclk,sync,En,
input[11:0]I_Actual,ref,
input[11:0]pasos,
output [11:0]SControl1,SControl2,SControl3,
output reg CtrlOk=1
    );
reg EnControl1=0;
Ctrl_busquedaMax c1(.sysclk(sysclk),.sync(sync),.EnControl(EnControl1),.I_Actual(I_Actual),
.pasos(pasos),.SControl(SControl1),.CicloOk(CicloOk1));  

reg EnControl2=0;
Ctrl_busquedaMax c2(.sysclk(sysclk),.sync(sync),.EnControl(EnControl2),.I_Actual(I_Actual),
.pasos(pasos),.SControl(SControl2),.CicloOk(CicloOk2));  

reg EnControl3=0;
Ctrl_busquedaMax c3(.sysclk(sysclk),.sync(sync),.EnControl(EnControl3),.I_Actual(I_Actual),
.pasos(pasos),.SControl(SControl3),.CicloOk(CicloOk3));  

OneShot osN(.sysclk(sysclk), .signal(sync),.Os(osync)); 
reg[3:0]index=0;
always@(posedge sysclk)begin
    case(index)
    0:begin
         if(osync & En) begin CtrlOk<=0; index<=1; end   
     end
    1:begin
        if(I_Actual<ref)begin  
        CtrlOk<=0;  index<=2; EnControl1<=1;
        end
        else begin
        CtrlOk<=1;  index<=0; 
        end
     end
     2:begin
        if(osync) index<=3; 
     end
     3:begin
         EnControl1<=0;
        if(CicloOk1) index<=0; // index<=4; 
     end
//     ////////////
//  4:begin
////        if(I_Actual>ref)begin  
//        CtrlOk<=0;  index<=5; EnControl2<=1;
////        end
////        else begin
////        CtrlOk<=1;  index<=0; 
////        end
//     end
//    5:begin
//        if(osync) index<=6; 
//     end
//     6:begin
//         EnControl2<=0;
//         if(CicloOk2) index<=7; 
//     end
    
    
//    /////////////////
//    7:begin
////        if(I_Actual>ref)begin  
//        CtrlOk<=0;  index<=8; EnControl3<=1; 
////        end
////        else begin
////        CtrlOk<=1;  index<=0; 
////        end
//     end
//     8:begin
//        if(osync) index<=9; 
//     end
//     9:begin
//         EnControl3<=0;
//         if(CicloOk3) index<=0; 
//     end
     endcase
end 

    
endmodule
