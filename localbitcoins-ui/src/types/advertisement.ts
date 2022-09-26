import { Profile } from "./profile"

export class Advertisement {
    data: AdvertisementData
    actions: AdvertisementAction
}

export class AdvertisementData {
    profile: Profile
    currency: string
    min_amount: string
    online_provider: string
    min_amount_available: string
    max_amount_available: string
    max_amount_available_usd: number
    temp_price_usd: string
    temp_price: string
    bank_name: string
    reference_percentage: number
    created_at: string
    is_good_deal: boolean
    is_me: boolean
}

export class AdvertisementAction {
    public_view: string
}