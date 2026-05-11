`timescale 1ns / 1ps
module Control_Rx(
input clk_100Mhz,Tick, ///reloj a 100MHz, y los tick del UART
input RsRx, /// Entrada de datos desde el UART
//Control Cuentas
output reg [15:0] amplificador=16'b1000100010001000,///?????
output reg [31:0]TcontrolStable=1000,Tphase=255,pAlgControl={1'd0,3'd4,12'd500,8'd10,3'b111,5'd10},ordenTx=32'd0,Pmuestreo=32'd5000, //// ?????
output reg [35:0] dataF_Rx={12'd0000,12'd0000,12'd000}, //////?????
output reg[11:0]trigger={4'd0,8'd128}, /////?????
 output reg [11:0] Amplificar=1 ///??
 );


wire[7:0]BufferRx; ///cable que almacena la data
UART_Rx RX01 (
    .clk_100Mhz(clk_100Mhz), 
    .Tick(Tick), 
    .RsRx(RsRx), 
    .RxOk(RxOk), 
    .BufferRx(BufferRx)
   ); //// RsOk es una bandera que indica cuando el UART esta listo para recibir información
   
OneShot osN(.sysclk(clk_100Mhz), .signal(RxOk),.Os(oRxOk));  // oneshot del clock a 100[MHz] el cual tiene como entrada la bandera del UART  y envia una señal de salida denominada oRxOk
 
 
reg [7:0]ControlRx=0; // variable declarada para hacer un siclo case, en este caso se requiere un index de 8 bit
reg [7:0]B_07_00=0,B_15_08=0; 
reg [7:0]B_23_16=0,B_31_24=0;
always @(posedge clk_100Mhz)begin

	case(ControlRx[7:0])
			0: begin 
				if(oRxOk)
				ControlRx[7:0]<=BufferRx[7:0]; // primer caso el index dependerá de la data enviada por el UART
			end
			
			1:begin  // se activa cuando se envia un 1 binario en el UART
				if(oRxOk) begin
				ControlRx[7:0]<=0; // el index vuelve a 0
				B_07_00[7:0]<=BufferRx[7:0]; // el sistema esperará que el UART envíe otro dato para reflejarlo en este caso en la variable B_07_00 para luego volver al index 0
				end
			end
			2: begin // se activa cuando se envia en el UART un número 2 en binario
				if(oRxOk) begin
				ControlRx[7:0]<=0; // index vuelve a 0
				B_15_08[7:0]<=BufferRx[7:0]; // con el numero 2 el UART almacena la data en la variable B_15_08
				end
			end
			3:begin // se activa cuando el UART envia un numero 3 en binario
				if(oRxOk) begin
				ControlRx[7:0]<=0; 
				B_23_16[7:0]<=BufferRx[7:0]; // data enviada en UART guarda la data en B_23_16
				end
			end
			4:begin // se activa cuando el UART envia un numero 4 en binario
				if(oRxOk) begin
				ControlRx[7:0]<=0;
				B_31_24[7:0]<=BufferRx[7:0]; // data enviada en UART guarda la data en B_23_16
				end
			end
			//////desde10  

//			11:begin
//				Tphase<={B_31_24, B_23_16,B_15_08,B_07_00};
//				ControlRx[7:0]<=0;
//			end     
			
//			wire[3:0] ctrlIndex=SignalTriguer[11:8];
//wire[7:0] UmbralIndex=SignalTriguer[7:0];
			12:begin
				ordenTx[15:8]<=B_07_00[7:0];  // desde el index 12-13-14 se escoge en que Tx se guarda la data de B_07_00
				ControlRx[7:0]<=0;
			end
			13:begin
				ordenTx[19:16]<=B_07_00[3:0]; // ¿Porque aqui se almacena una data de solo 4 bit?
				ControlRx[7:0]<=0;
			end
			14:begin
				ordenTx[7:0]<=B_07_00[7:0];
				ControlRx[7:0]<=0;
			end  
			
			21:begin
				amplificador[15:0]<= {B_15_08,B_07_00}; //variable amplificador guarda la data de B_15_08,B_07_00
				ControlRx[7:0]<=0;
			end    
			 
			 30:begin 
			 Pmuestreo<={B_31_24, B_23_16,B_15_08,B_07_00}; // almacena toda la data de la variable B en un solo vector
			 ControlRx[7:0]<=0;
			 end  
			  31:begin 
			  dataF_Rx[11:0]<={B_15_08[3:0],B_07_00};
			 ControlRx[7:0]<=0;
			 end  
			  32:begin 
			  dataF_Rx[23:12]<={B_15_08[3:0],B_07_00};
			 ControlRx[7:0]<=0;
			 end  
			 33:begin 
			 dataF_Rx[35:24]<={B_15_08[3:0],B_07_00};
			 ControlRx[7:0]<=0;
			 end  
        
        	 39:begin
				pAlgControl[4:0]<=B_07_00[4:0];//Algcontrol
				ControlRx[7:0]<=0;
			end
			40:begin
				pAlgControl[7:5]<=B_07_00[2:0];//Algcontrol
				ControlRx[7:0]<=0;
			end			
			41:begin
				pAlgControl[15:8]<=B_07_00;//pasos
				ControlRx[7:0]<=0;
			end
			42:begin
				pAlgControl[27:16]<={B_15_08[3:0],B_07_00};//referencia
				ControlRx[7:0]<=0;
			end
			43:begin
				pAlgControl[30:28]<=B_07_00[2:0];//Selectro de Ref
				ControlRx[7:0]<=0;
			end
			44:begin
				pAlgControl[31]<=B_07_00[0];//ModOn
				ControlRx[7:0]<=0;
			end
			
			45:begin
				Amplificar[11:0]<={B_15_08[3:0],B_07_00};//referencia
				ControlRx[7:0]<=0;
			end
			46:begin
				trigger[11:0]<={B_15_08[3:0],B_07_00};//referencia
				ControlRx[7:0]<=0;
			end
			47:begin
			Tphase<={B_31_24, B_23_16,B_15_08,B_07_00};
				ControlRx[7:0]<=0;
			end
			 48:begin
			TcontrolStable<={B_31_24, B_23_16,B_15_08,B_07_00};
				ControlRx[7:0]<=0;
			end
			
			
			98:begin
				ordenTx[7:0]<=0;
				ControlRx[7:0]<=0;
			end     
			default begin ControlRx[7:0]<=0; end
	endcase
end


endmodule
