`timescale 1ns / 1ps

module Selector_Intensidad(
input sysclk, 
input [2:0]selector,
input [48:0]dataADC,
output reg[11:0] I_Actual=0
    );
// Selector_Intensidad SI01(.sysclk(sysclk),.selector(selector),.dataADCtx(dataADCtx),.I_Actual(I_Actual)  );   
    
wire[11:0]I1=dataADC[11:00];   	
wire[11:0]I2=dataADC[23:12];   
wire[11:0]I3=dataADC[35:24]; 	
wire[11:0]I4=dataADC[47:36];   
 
wire[11:0] S1=(I2/4+I3/4+I4/4);
wire[11:0] S2=(I1/4+I3/4+I4/4);
wire[11:0] S3=(I2/4+I1/4+I4/4);
wire[11:0] S4=(I2/4+I3/4+I1/4); 

//assign dataADCtx[31:24]=dataADC[47:40];
//assign dataADCtx[23:16]=dataADC[35:28];
//assign dataADCtx[15:08]=dataADC[23:16];
//assign dataADCtx[07:00]=dataADC[11:04];

always@(posedge sysclk)begin
    case(selector)
    0:begin I_Actual[11:0]<=S1; end //I1
    1:begin I_Actual[11:0]<=S2; end //I2
    2:begin I_Actual[11:0]<=S3; end //I3
    3:begin I_Actual[11:0]<=S4; end //I4
    4:begin I_Actual[11:4]<=dataADC[47:40];end
    5:begin I_Actual[11:4]<=dataADC[35:28];end //I2
    6:begin I_Actual[11:4]<=dataADC[23:16];end
    7:begin I_Actual[11:4]<=dataADC[11:04];end //I4
//    4:begin I_Actual[11:0]<=I3; end //I1
//    5:begin I_Actual[11:0]<={I3[11:4],4'd0}; end //I2
//    6:begin I_Actual[11:0]<=I4; end //I3
//    7:begin I_Actual[11:0]<={I4[11:4],4'd0}; end //I4
    endcase
end
endmodule
