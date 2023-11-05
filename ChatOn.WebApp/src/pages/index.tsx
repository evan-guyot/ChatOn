import styles from "@/styles/page.module.css";
import LoginIcon from "@mui/icons-material/Login";
import PersonAddIcon from "@mui/icons-material/PersonAdd";
import { Button, Typography } from "@mui/material";

export default function Home() {
  return (
    <main className={styles.main}>
      <div className={styles.center}>
        <Typography variant="h1" className={styles.welcome}>
          Welcome on Chat On
        </Typography>
        <div className={styles.buttonContainer}>
          <Button
            variant="contained"
            className={styles.button}
            href="signup"
            endIcon={<PersonAddIcon />}
          >
            Sign up
          </Button>
          <Button
            variant="outlined"
            className={styles.button}
            href="login"
            endIcon={<LoginIcon />}
          >
            Log in
          </Button>
        </div>
      </div>
    </main>
  );
}
