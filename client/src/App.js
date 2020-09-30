import React, { useState } from "react";

import "./App.css";
import Game from "./Game";

function App() {
  const [playerName, setPlayerName] = useState("Tal");
  const [submitted, setSubmitted] = useState(false);

  if (submitted) {
    return <Game playerName={playerName} />;
  }

  return (
    <div className="centerFlex column">
      <div></div>
      <div>
        <h2 className="title">Before we begin, we need your name</h2>
        <form
          onSubmit={() => setSubmitted(true)}
          style={{ display: "flex" }}
          className="column"
        >
          <input
            placeholder="Who are you?"
            className="inputBig"
            onChange={(e) => {
              setPlayerName(e.target.value);
            }}
          ></input>
          <button type="submit" style={{ marginTop: "10px" }}>
            Let me play!
          </button>
        </form>
      </div>
      <div></div>
    </div>
  );
}

export default App;
