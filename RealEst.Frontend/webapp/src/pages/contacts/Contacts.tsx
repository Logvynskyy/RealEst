import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Contact } from "../../interfaces/Contact";
import { Endpoints } from "../../constants/endpoints";
import { PencilIcon, TrashIcon } from "lucide-react";

const Contacts = () => {
  const [contacts, setContacts] = useState<Contact[]>();
  const navigate = useNavigate();

  const getContacts = async () => {
    try {
      const response = await fetch("https://localhost:7115/api/Contacts", {
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

      setContacts(responseData);
    } catch (error) {
      console.error("Error getting contacts:", error);
      return [];
    }
  };

  useEffect(() => {
    getContacts();
  }, []);

  const createContact = async () => {
    try {
      navigate(Endpoints.createContact);
    } catch (error) {
      console.error("Redirect error:", error);
    }
  };

  const deleteContact = async (id: number) => {
    try {
      const response = await fetch(
        "https://localhost:7115/api/Contacts/delete/" + id,
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

      setContacts((contacts) => contacts?.filter((contact) => contact.id != id));
    } catch (error) {
      console.error("Error deleting contact:", error);
      return [];
    }
  };

  return (
    <div className="flex w-full min-h-screen p-10 justify-center bg-[#6DDCFF]">
      <div className="w-full p-2 bg-white gap-y-4 relative">
        <div className="flex flex-col justify-items-start p-2 bg-white gap-y-4">
          {contacts?.map((contact, index) => (
            <div
              key={index}
              className="flex items-center justify-between min-w-full
                        hover:bg-gray-500 bg-gray-400 py-2 px-4 rounded-md font-medium text-white text-3xl"
            >
              <Link to={`${Endpoints.contact + "/" + contact.id}`} className="w-full">
                <div className="flex items-center">
                  <div>{contact.displayString}</div>
                </div>
              </Link>
              <div className="flex gap-2 items-center z-20">
                <Link
                  to={`${Endpoints.editContact + "/" + contact.id}`}
                  className="bg-white"
                >
                  <PencilIcon className="stroke-black" />
                </Link>
                <div
                  className="bg-white z-20 cursor-pointer"
                  onClick={() => deleteContact(contact.id)}
                >
                  <TrashIcon className="stroke-black" />
                </div>
              </div>
            </div>
          ))}
        </div>
        <button
          onClick={createContact}
          className="bg-blue-500 text-white py-2 px-4 rounded-md hover:bg-blue-700 
                    focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500
                    absolute bottom-5 right-5 p-4 text-2xl"
        >
          Додати контакт
        </button>
      </div>
    </div>
  );
};

export default Contacts;
