`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    14:13:14 07/05/2015 
// Design Name: 
// Module Name:    ClockUART 
// Project Name: 
// Target Devices: 
// Tool versions: 
// Description: 
//
// Dependencies: 
//
// Revision: 
// Revision 0.01 - File Created
// Additional Comments: 
//
//////////////////////////////////////////////////////////////////////////////////
module ClockUART(
input clock,
output reg UARTclock
    );
	 
reg [5:0]regCountClkTick=0;

always @(posedge clock)begin  //crea el clock de baudios y clock de Ticks
		
		regCountClkTick <=regCountClkTick +6'd1;
		//200M/(16*115,2)=108,5  n=F/(16*Baudios) 27=50M/(16*115,2k) 54=100M/(16*115,2k) 
		if(regCountClkTick > 54)begin//@100M
		regCountClkTick <=6'd1;
		UARTclock<=1'd1;
		end
		else
		UARTclock<=1'd0;
end

endmodule
