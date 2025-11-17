export interface Client {
  id?: number;
  name: string;
  phoneNumber: string;
  email: string;
  address: string;
  createdDate: string;
}

export interface CreateClientDto {
  name: string;
  phoneNumber: string;
  email: string;
  address: string;
}

export interface UpdateClientDto {
  name?: string;
  phoneNumber?: string;
  email?: string;
  address?: string;
}
