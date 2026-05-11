`timescale 1ns / 1ps
module UART_clock(
input clk_100Mhz,
output reg Tick=0
    );
	 
reg [6:0]countTick=0;

always @(posedge clk_100Mhz)begin  //crea el clock de baudios y clock de Ticks
		
 	//200M/(16*115,2)=108,5  n=F/(16*Baudios) 27=50M/(16*115,2k) 54.25=100M/(16*115.2k)
 	//67,8=125M/(16*115,2k) 
		if(countTick > 67)begin//@100M
		countTick <=7'd1;
		Tick<=1'd1;
		end
		else begin
		Tick<=1'd0;
		countTick <=countTick +7'd1;
		end
end

endmodule
