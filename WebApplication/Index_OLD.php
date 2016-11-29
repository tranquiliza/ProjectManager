<!DOCTYPE html>
<html>
<head>
    <title></title>
	<meta charset="utf-8" />
	<style>
	table, th, td {
		border: 1px solid black;
		border-collapse: collapse;
	}
	th, td {
		padding: 5px;
		text-align: left;
	}
	table#t01 {
		width: 100%;    
		background-color: #f1f1c1;
	}
	</style>
</head>
<body>

	<table style="width:100%">
		<tr>
			<th>ID</th>
			<th>Opgave</th> 
			<th>Action</th> 
			<th>Forventet Start</th> 
			<th>Personale</th> 
			<th>Pris</th> 
			<th>Status</th> 
			<th>Frist</th>
			<th>Afdeling</th>
		</tr>
	<?php
	$servername = "localhost";
	$username = "root";
	$password = "dalle123";
	$dbname = "ProjectManager";

	// Create connection
	$conn = mysqli_connect($servername, $username, $password, $dbname);
	// Check connection
	if (!$conn) {
		die("Connection failed: " . mysqli_connect_error());
	}
	
	//Execute SQL:
	$sql = "SELECT * FROM Opgaver";
	$result = $conn->query($sql);
	
	if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
		echo "
			<tr>
				<td>". $row["OpgaveID"]."</td>
				<td>". $row["OpgaveNavn"]."</td>
				<td>". $row["Action"]."</td>
				<td>". $row["Forventet Start"]."</td>
				<td>". $row["Personale"]."</td>
				<td>". $row["Pris"]."</td>
				<td>". $row["Status"]."</td>
				<td>". $row["Frist"]."</td>
				<td>". $row["Afdeling"]."</td>
			</tr>
			";
        //echo "id: " . $row["OpgaveID"]. " - Name: " . $row["OpgaveNavn"]. " " . $row["Action"]. "<br>";
    }
	} else {
		//If error happens we can do stuff with that?
	}
	$conn->close();
	?>
	</table>
</body>
</html>
