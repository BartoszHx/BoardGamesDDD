import React from "react";
import { Link } from "react-router-dom";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import styles from "./Navbar.module.css";

const Navbar = React.forwardRef((props, ref) => {
    const appBarColor = "saddlebrown";

    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static" className={styles.appbar} sx={{ background: appBarColor  }}>
                <Toolbar>
                    <Typography variant="h5" sx={{ flexGrow: 1 }}>
                        <Link className={styles.link} to="new-game">
                            New Game
                        </Link>
                        <Link className={styles.link} to="load-game">
                            Load Game
                        </Link>
                        <Link className={styles.link} to="testpage">
                            testpage
                        </Link>
                    </Typography>
                </Toolbar>
            </AppBar>
        </Box>
    );
});

Navbar.displayName = "Navbar";

export default Navbar;