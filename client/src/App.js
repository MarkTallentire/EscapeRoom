import React, { useEffect, useState } from "react";
import logo from "./logo.svg";
import "./App.css";
import * as SignalR from "@microsoft/signalr";
import Axios from "axios";

import cardOne from "./images/1.png";

function App() {
  // Builds the SignalR connection, mapping it to /chat

  const [hubConnection, setHubConnection] = useState(null);
  const [cards, setCards] = useState(null);
  const [startTime, setStartTime] = useState(null);
  const [endTime, setEndTime] = useState();

  useEffect(() => {
    if (startTime) {
      setInterval(() => {
        const difference = +new Date() - +new Date(startTime);
        const minutes =
          60 - Math.floor((difference % (1000 * 60 * 60)) / (1000 * 60));
        const seconds = 60 - Math.floor((difference % (1000 * 60)) / 1000);
        setEndTime(
          `${minutes} ${
            seconds === 60 ? "00" : seconds.length != 2 ? seconds : seconds
          }`
        );
      }, 1000);
    }
  }, [startTime]);

  useEffect(() => {
    const connection = new SignalR.HubConnectionBuilder()
      .withUrl("https://localhost:44349/gamehub")
      .configureLogging(SignalR.LogLevel.Information)
      .build();

    setHubConnection(connection);
  }, []);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.start().then((a) => {
        // Once started, invokes the sendConnectionId in our ChatHub inside our ASP.NET Core application.
        if (hubConnection.connectionId) {
          hubConnection.invoke("joinGame", hubConnection.connectionId);
        }
        hubConnection.invoke("getDisplayed");
        hubConnection.on("getDisplayed", (message) => {
          setCards(message);
        });
        hubConnection.invoke("getTime");
        hubConnection.on("getTime", (time) => {
          setStartTime(time);
        });
      });
    }
  }, [hubConnection]);

  return (
    <div className="App">
      {!startTime ? "Start Game" : endTime}
      {cards && cards.map((card) => card.number === 1 && <img src={cardOne} />)}
      <AdminPanel hubConnection={hubConnection} />
    </div>
  );
}

export default App;

export const AdminPanel = ({ hubConnection }) => {
  const onClick = () => {
    hubConnection.invoke("hideCard", 1);
  };

  const addToDisplay = () => {
    hubConnection.invoke("showCard", 1);
  };

  return (
    <>
      <button onClick={addToDisplay}>Add Card</button>
      <button onClick={onClick}>Remove Card</button>
    </>
  );
};
