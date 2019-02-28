<?php
 function get_name($Sezione,$Reparto)
 {
	$str = file_get_contents('http://localhost/Lolli/Tp/books.json');
	$books = json_decode($str, true); 
	$result = array();
	$count = 0;
	foreach($books as $book)
	{
		//Ottiene il titolo in base al reparto e alla sezione
		if($book['Sezione']==$Sezione && $book['Reparto']==$Reparto)
		{
			//array_push($result,$book['Titolo']);
			$count++
			
		}
	}
	array_push($result,$count);
	return $result;
 }
 
 
 
 function get_Discount()
 {
	$str = file_get_contents('http://localhost/Lolli/Tp/books.json');
	$books = json_decode($str, true);	 
	$result = array();
	$b = array();
	$s = array();
	//Ordina L'array
	$a=	array_sort($books,"Sconto");
	
	foreach($a as $book)
	{
		//Inserisce libri e sconti nei rispettivi array 
		array_push($b,$book['Titolo']);
			 
		array_push($s,$book['Sconto']);	 
	}
	//inserisce gli array dei libri e degli sconti nel' array risultante mantenendo gli indici
	array_push($result,$b,$s);
	return $result;
 }
 
 function Get_Date($data1,$data2)
 {
	$str = file_get_contents('http://localhost/Lolli/Tp/books.json');
	$books = json_decode($str, true); 
	
	$result = array();	

	foreach($books as $book)
	{
		//Ritorna i titoli compresi tra due date
		if(diff($book['Data'],$data1))
		{
			
			if(diff($data2,$book['Data']))
				array_push($result,$book['Titolo']);
		}
	}

	return $result;
 }
 
 function Get_Cart($id)
 {
	$str = file_get_contents('http://localhost/Lolli/Tp/dbUtenti.json');
	$utenti = json_decode($str, true);
	$result = array();
	$b = array();
	$a = array();
	
	foreach($utenti as $utente)
	{
		//Controlla l'utente inserito
		if($utente['Id'] == $id)
		{
			array_push($result,$utente['Email']);
			//Ritorna il carrello
			foreach($utente['Cart'] as $c)
			{
				//ritorna i libri e la relativa quantità
				array_push($b,$c['Book'][0]['BookId']);
					  
				array_push($a,$c['Book'][0]['Amount']);	
				
			}
		} 
	}
	array_push($result,$b,$a);
	return $result;
 }
 
 
 
 
 
 
 
 
 
 
 
 function diff($d1,$d2)
 {
	list($day1,$month1,$year1) = explode('/', $d1);
	list($day2,$month2,$year2) = explode('/', $d2);	
	//Confronta le due date
	if($year1 > $year2)
		{			 			 				
			return true;
		}
		if($year1 == $year2 && $month1 > $month2)
		{
				return true;
		}
		if($year1 == $year2 && $month1 == $month2 && $day1 >= $day2)
		{					
			return true;
		}
 }
 
function array_sort($array, $on, $order=SORT_ASC)
{
    $new_array = array();
    $sortable_array = array();

    if (count($array) > 0) {
        foreach ($array as $k => $v) {
            if (is_array($v)) {
                foreach ($v as $k2 => $v2) {
                    if ($k2 == $on) {
                        $sortable_array[$k] = $v2;
                    }
                }
            } else {
                $sortable_array[$k] = $v;
            }
        }

        switch ($order) {
            case SORT_ASC:
                asort($sortable_array);
            break;
            case SORT_DESC:
                arsort($sortable_array);
            break;
        }

        foreach ($sortable_array as $k => $v) {
            $new_array[$k] = $array[$k];
        }
    }

    return $new_array;
}

?>