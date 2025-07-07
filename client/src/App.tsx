import { useEffect, useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
// import "./App.css";
import { ListItem, Typography } from "@mui/material";
import axios from "axios";

function App() {
  const [count, setCount] = useState(0);
  const [data, setData] = useState<any[]>([]);

  useEffect(() => {
    axios.get<any>("https://localhost:5001/api/activities")
      .then((response) => {
        setData(response.data);
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  }, []);

  return (
    <div>
      <Typography variant="h3">Reactivities</Typography>  
      {data?.map((item, index) => (
       <ListItem key={index}>
          <Typography >{item.title}</Typography>
        </ListItem>
      ))}
    </div>
  );
}

export default App;
