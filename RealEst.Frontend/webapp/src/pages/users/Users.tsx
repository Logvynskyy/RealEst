import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { User } from "../../interfaces/User";
import { Endpoints } from "../../constants/endpoints";
import { TrashIcon } from "lucide-react";

const Users = () => {
  const [users, setUsers] = useState<User[]>();
  const navigate = useNavigate();

  const getUsers = async () => {
    try {
      const response = await fetch("https://localhost:7115/api/Authentication/Users", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("JWTToken"),
        },
      });

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      const responseData = await response.json();
      console.log(responseData);

      setUsers(responseData);
    } catch (error) {
      console.error("Error getting users:", error);
      return [];
    }
  };

  useEffect(() => {
    getUsers();
  }, []);

  const createUser = async () => {
    try {
      navigate(Endpoints.createUser);
    } catch (error) {
      console.error("Redirect error:", error);
    }
  };

  const deleteUser = async (username: string) => {
    try {
      const response = await fetch(
        "https://localhost:7115/api/Authentication/Users/delete/" + username,
        {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + localStorage.getItem("JWTToken"),
          },
        }
      );

      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      setUsers((users) => users?.filter((user) => user.username != username));
    } catch (error) {
      console.error("Error deleting user:", error);
      return [];
    }
  };

  return (
    <div className="flex w-full min-h-screen p-10 justify-center bg-[#6DDCFF]">
      <div className="w-full p-2 bg-white gap-y-4 relative">
        <div className="flex flex-col justify-items-start p-2 bg-white gap-y-4">
          {users?.map((user, index) => (
            <div
              key={index}
              className="flex items-center justify-between min-w-full
                        hover:bg-gray-500 bg-gray-400 py-2 px-4 rounded-md font-medium text-white text-3xl"
            >
              <Link to={`${Endpoints.user + "/" + user.username}`} className="w-full">
                <div className="flex items-center">
                  <div>{user.username}</div>
                </div>
              </Link>
              <div className="flex gap-2 items-center z-20">
                <div
                  className="bg-white z-20 cursor-pointer"
                  onClick={() => deleteUser(user.username)}
                >
                  <TrashIcon className="stroke-black" />
                </div>
              </div>
            </div>
          ))}
        </div>
        <div>
        <button
          onClick={createUser}
          className="bg-blue-500 text-white py-2 px-4 rounded-md hover:bg-blue-700 
                    focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500
                    absolute bottom-5 right-5 p-4 text-2xl"
        >
          Додати користувача
        </button>
        </div>
      </div>
    </div>
  );
};

export default Users;
