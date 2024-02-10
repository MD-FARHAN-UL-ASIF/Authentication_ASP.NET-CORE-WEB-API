// Login.jsx

import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';
import './Login.css'; // Import the Login.css file

const Login = () => {
    const [loading, setLoading] = useState(false);
    const { register, handleSubmit, formState: { errors } } = useForm();

    const onSubmit = async (data) => {
        setLoading(true);

        try {
            const response = await axios.post("https://localhost:7002/api/Account/login", data, {
                headers: { 'Content-Type': 'application/json' }
            });

            console.log(response.data);
            console.log(response.data.result.accessToken);

            if (Object.keys(response.data).length === 0) {
                alert("Login failed");
            } else {
                alert("Login successful");
                sessionStorage.setItem('accessToken', response.data.result.accessToken);
                // Redirect or handle success as needed
            }
        } catch (error) {
            console.error('Login Failed due to:', error.message);
            alert('Login Failed due to:' + error.message);
        }

        setLoading(false);
    };

    return (
        <div className="login-container">
            <form onSubmit={handleSubmit(onSubmit)} className="login-form">
                <label>Email</label>
                <input type="email" {...register("Email", { required: true })} />
                {errors.Email && <span>Email is required</span>}

                <label>Password</label>
                <input type="password" {...register("Password", { required: true })} />
                {errors.Password && <span>Password is required</span>}

                <input type="submit" disabled={loading} />
            </form>
        </div>
    );
};

export default Login;
