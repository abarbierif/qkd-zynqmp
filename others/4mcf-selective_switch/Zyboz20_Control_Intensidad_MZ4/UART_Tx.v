`timescale 1ns / 1ps
module UART_Tx(
input Tick,clk_100Mhz,
input [7:0] BuferTx,
input StartTx,
output reg readyTx=1,
output reg  RsTx=1
);


	//egistros locales
reg [1:0] regBloque=0; //registro valor de entrada a cada bloque
reg [2:0] regCountBTx=0; 	//registro Cuenta Bit enviado
reg [3:0] regCountTick=0;		//16  decimal necesita solo 4 bit

always @(posedge clk_100Mhz)begin

if(Tick)begin
	
	case(regBloque)
	
	2'b00: begin	
		if(StartTx)begin
			regBloque=2'b01;
			regCountTick=0;
			readyTx=0;
			RsTx=1'b1;
		end
	end
	2'b01:	begin	
		
				if(regCountTick>=15)begin
				regBloque=2'b10;
				regCountTick=0;
				regCountBTx=0;
				
				
				end
				else begin
				regCountTick=regCountTick + 4'b1;
				RsTx=0;
				end
	end
		
	2'b10:	begin	
		
			if(regCountTick==15)begin
				regCountTick=0;
				
					if(regCountBTx==7) //ya que son 8 bit
					regBloque=2'b11;
					else
					regCountBTx=regCountBTx+3'b1;
					
			end
			
			else begin
				regCountTick=regCountTick+4'b1;
				RsTx=BuferTx[regCountBTx];
			end
	end
	
	2'b11:		begin
			if(&regCountTick)begin
			regBloque=2'b00;
			readyTx = 1'b1;
			end
			else begin
			regCountTick=regCountTick+4'b1;
			RsTx=1'b1;
			end
	end	
	
	endcase
end
end

endmodule


 
