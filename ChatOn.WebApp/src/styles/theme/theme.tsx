import { createTheme } from "@mui/material/styles";

const darkTheme = createTheme({
  palette: {
    primary: {
      main: "#e1e1e1",
    },
    secondary: {
      main: "#1e1e1e",
    },
    error: {
      main: "#ef4444",
    },
    info: {
      main: "#3b82f6",
    },
    success: {
      main: "#22c55e",
    },
    warning: {
      main: "#f97316",
    },
  },
  typography: {
    fontFamily: "system-ui",
  },
});

export default darkTheme;
