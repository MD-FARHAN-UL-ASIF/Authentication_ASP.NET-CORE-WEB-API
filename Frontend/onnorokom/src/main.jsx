import React from 'react';
import ReactDOM from 'react-dom';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import App from './App.jsx';
import './index.css';
import Home from './components/Home/Home.jsx';
import Dashboard from './components/Dashboard/Dashboard.jsx';
import Login from './components/Login/Login.jsx';
import Registration from './components/Registration/Registration.jsx';
import CreateCategory from "./components/CreateCategory/CreateCategory.jsx";
import ShowCategory from './components/ShowCategory/ShowCategory.jsx';
import CreateQuestion from './components/Nav/CreateQuestion/CreateQuestion.jsx';

const router = createBrowserRouter([
  {
    path: '/',
    element: <Home />,
  },
  {
    path: '/dashboard',
    element: <Dashboard />,
  },
  {
    path: '/login',
    element: <Login />,
  },
  {
    path: '/registration',
    element: <Registration />,
  },
  {
    path: '/create-category',
    element: <CreateCategory />,
  },
  {
    path: '/show-category',
    element: <ShowCategory />,
  },
  {
    path: '/create-question',
    element: <CreateQuestion />,
  },
]);

ReactDOM.render(
  <React.StrictMode>
    <RouterProvider router={router}>
      <App />
    </RouterProvider>
  </React.StrictMode>,
  document.getElementById('root')
);
