import React from 'react';
import logo from './logo.svg';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import './App.css';
import ChessGame from "./pages/ChessGame";
import NotFound from "./pages/NotFound";
import Navbar from './components/Navbar';
import NewGame from './pages/NewGame';
import LoadGame from './pages/LoadGame';

function App() {
    return (
        <BrowserRouter>
            <Navbar/>
            <div className="App">
                <Routes>
                    <Route path="chess/:id" element={<ChessGame />} />
                    <Route path="*" element={<NotFound />} />
                    <Route path="new-game" element={<NewGame />} />
                    <Route path="load-game" element={<LoadGame />} />
                </Routes>
            </div>
      </BrowserRouter>
  );
}

export default App;
