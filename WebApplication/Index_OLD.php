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
	<table style="width:80%" align="center">
		<tr>
			<th>ID</th>
			<th>Opgave</th> 
			<th>Action</th> 
			<th>Forventet Start</th> 
			<th>Personale</th> 
			<th>Pris</th> 
			<th>Igang</th> <!-- Placeholder -->
			<th>Standby</th> <!-- Placeholder -->
			<th>Færdig</th> <!-- Placeholder -->
			<th>Afventer Godk.</th> <!-- Placeholder -->
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
			echo "<tr>";
			echo 	"<td>". $row["OpgaveID"]."</td>";
			echo		"<td>". $row["OpgaveNavn"]."</td>";
			echo		"<td>". $row["Action"]."</td>";
			echo		"<td>". $row["Forventet Start"]."</td>";
			echo		"<td>". $row["Personale"]."</td>";
			if($row["Pris"] != 0)
			{
				echo		"<td>". $row["Pris"]."</td>";
			}
			else
			{
				echo "<td>Ingen Pris</td>";
			}
			echo		"<td>". $row["Status"]."</td> <!-- Placeholder -->";
			echo		"<td>". $row["Status"]."</td> <!-- Placeholder -->";
			echo		"<td>". $row["Status"]."</td> <!-- Placeholder -->";
			echo		"<td>". $row["Status"]."</td> <!-- Placeholder -->";
			echo		"<td>". $row["Frist"]."</td>";
			echo		"<td>". $row["Afdeling"]."</td>";
			echo		"<td>". $row["Underopgave"]."</td>";
			echo	"</tr>";
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
