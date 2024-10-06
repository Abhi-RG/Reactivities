import { useEffect, useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
// import "./App.css";
import axios from "axios";
import { Header } from "semantic-ui-react";

function App() {
  const [activities, setActivities] = useState([]);
  useEffect(() => {
    axios.get("http://localhost:5000/api/Activities").then((response) => {
      setActivities(response.data);
    });
  }, []);



  return (
    <>
      <Header>Reactivities</Header>
      {activities.map((i:any)=>(
        <p>{i.title}</p>
      ))}

      
    </>
  );
}

export default App;
