<?php
 function get_name($Sezione,$Reparto)
 {
	$str = file_get_contents('http://localhost/Lolli/Tp/books.json');
	$books = json_decode($str, true); 
	$result = array();
	
	foreach($books as $book)
	{
		if($book['Sezione']==$Sezione && $book['Reparto']==$Reparto)
		{
			array_push($result,$book['Titolo']);
			 
		}
	}
	return $result;
 }
 
 
 
 function get_Discount()
 {
	$str = file_get_contents('http://localhost/Lolli/Tp/books.json');
	$books = json_decode($str, true);	 
	$a=	array_sort($books,"Sconto");
	$result = array();
	$b = array();
	$s = array();
	
	foreach($a as $book)
	{
		 
		array_push($b,$book['Titolo']);
			 
		array_push($s, $book['Sconto']);	 
	}
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
		if($utente['Id'] == $id)
		{
			array_push($result,$utente['Email']);
			foreach($utente['Cart'] as $c)
			{
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
	
	if($year1 > $year2)
		{			 			 				
			return true;
		}
		elseif($year1 == $year2 && $month1 > $month2)
		{
				return true;
		}
		else if($year1 == $year2 && $month1 == $month2 && $day1 >= $day2)
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