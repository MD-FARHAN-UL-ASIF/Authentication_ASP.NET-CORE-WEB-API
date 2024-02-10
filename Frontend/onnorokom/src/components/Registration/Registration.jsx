import React from 'react';
import { useForm } from 'react-hook-form';
import axios from 'axios';

const Registration = () => {
    const { register, handleSubmit, formState: { errors } } = useForm();

    const onSubmit = async (data) => {
        try {
            const response = await axios.post(`https://localhost:7002/api/Account/register?roleName=${data.RoleName || "Student"}`, data, {
                headers: { 'Content-Type': 'application/json' }
            });

            console.log(response.data);

            if (Object.keys(response.data).length === 0) {
                alert("Registration failed");
            } else {
                alert("Registration successful");
                // Redirect or handle success as needed
            }
        } catch (error) {
            console.error('Registration Failed due to:', error.message);
            alert('Registration Failed due to:' + error.message);
        }
    };

    return (
        <div>
            <form onSubmit={handleSubmit(onSubmit)}>
                {/* Other form fields */}
                <label>Name</label>
                <input type="text" {...register("Name", { required: true })} />
                {errors.Name && <span>Name is required</span>}

                <label>Email</label>
                <input type="email" {...register("Email", { required: true })} />
                {errors.Email && <span>Email is required</span>}

                <label>Phone Number</label>
                <input type="text" {...register("PhoneNumber", { required: true })} />
                {errors.PhoneNumber && <span>Phone Number is required</span>}

                <label>Designation</label>
                <input type="text" {...register("Designation", { required: true })} />
                {errors.Designation && <span>Designation is required</span>}

                <label>Institution</label>
                <input type="text" {...register("Institution", { required: true })} />
                {errors.Institution && <span>Institution is required</span>}

                <label>Password</label>
                <input type="password" {...register("Password", { required: true })} />
                {errors.Password && <span>Password is required</span>}

                {/* Dropdown for Role Name */}
                <label>Role Name</label>
                <select {...register("RoleName", { required: true })}>
                    <option value="Admin">Admin</option>
                    <option value="Student">Student</option>
                    <option value="Teacher">Teacher</option>
                    <option value="Moderator">Moderator</option>
                </select>
                {errors.RoleName && <span>Role Name is required</span>}

                <input type="submit" />
            </form>
        </div>
    );
};

export default Registration;
