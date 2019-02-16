<?php
	header ("Content-Type_application/json");

	include("functions.php");
	
	if(!empty($_GET['op']))
	{
	
		$op=$_GET['op'];
			
	
		switch($op)
		{
			case 1:
			deliver_response(200,"Operation complete",get_name($_GET['sez'],$_GET['rep']));
			break;
			case 2:
				deliver_response(200,"Operation complete",get_Discount());
			break;
			case 3:
			deliver_response(200,"Operation complete",Get_Date($_GET['d1'],$_GET['d2']));
			
			break;
			case 4:
			 deliver_response(200,"Operation complete",Get_Cart($_GET['id']));
			break;
		}
		
		
		
		
		
		
		/*
		//book not found
			deliver_response(200,"book not found", NULL);
			else
			//respond book price
			deliver_response(200,"book found", $price);
				}*/
	}		
	else
	{
		//throw invalid request
		deliver_response(400,"Invalid request", NULL);
	}
	
	
	
	
	
	
	
	function deliver_response($status, $status_message, $data)
	{
		header("HTTP/1.1 $status $status_message");
		
		//$response ['status']=$status;
		//$response['status_message']=$status_message;
		$response=$data;
		
		$json_response=json_encode($response);
		echo $json_response;
	}

?>