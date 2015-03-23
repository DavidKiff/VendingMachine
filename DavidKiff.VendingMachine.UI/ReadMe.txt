Coding Assumptions:
 - We have the same coding style
 - Not "happy" about creating "CashCard.Balance", its only used for testing.  We can black-box test without, but its critical the balance is accurate to avoid potential money loss, so it's perhaps justified in this instance.
 - I would use FluentAssertions as it reads better, although didn't here to minimise dependencies and project size.
 - I would usually add logging for debugging issues later and for auditing purposes, left out for bevariety.
 - The transaction class could have a Rollback method, but it implements IDisposable. I think in this day and age, people would expect a transaction to rollback when disposed and not committed, so it's perhaps justified.
 - Transaction class is not threadsafe, although we only use it in a single stack frame, so that's acceptable.
 - Soft drink class created instead of "can" as its more generic and we can provide better implementations when we have clearer requirements. (I would rename the BuyCan method to BuySoftDrink, but that will break your requirements).
 - You haven't specified a return type for BuyCan, assuming you will want details on failure too.
 - Some people find multiple returns in a method hard to read/more bug prone, happy to follow convention.  The method is small, so it’s clear to me!
 - Could make "EmptyTransaction" static readonly, but then we need to think about concurrency.  This is simpler, but higher costs on memory.
 - Personally dislike the "<summary>" comments, happy to add, I feel self-describing code is better documentation.  Most people just use GhostDoc to state the obvious.
 - Could add interfaces to objects and use mocks to test, although seems an overkill for a small application.
 - Don't typically test get/set properties with no logic, such as DebitResult.
 - Could add loads more tests, they prove the requirements are met though.

Business Assumptions:
 - Cash card:
      - It holds the balance, in reality this would probably be held in a "central" database somewhere.
      - It's safe/allowed to debit the card, before the consumer has received their goods and refund them if we can't make the purchase.
      - Debiting cards is a cheap operation in terms of performance.
      - "Cash cards can be used in many places"... assuming they are used in the same process.  Not made a singleton as it says "cash card*s*".  Cash card lifetime to be managed by the consuming code.
      - Need to clarify "Can't vend if less than 50p on the card", is that after the purchase?  Assuming they must always have at least 50p on their card.

 - Vending machine:
     - It only allows consumers to buy "soft drinks", no implementations/types, such as coke, fanta etc.
     - All soft drinks are the same cost.
     - It's a use once vending machine, you can't refill stock.
     - We don’t accept FX currencies to buy soft drinks, its GBP only.
     - You will want to provide feedback about any failed purchases (for example, insufficient funds or no soft drinks left).
 
Future improvements:
  - Provide ability to replenish vending machine stock.
  - Allow the cash card to be used outside of this process.
  - The BuyCan method doesn’t allow a "selection" to be passed in, users might be unhappy receiving a random soft drink, rather than their selection? (The selection class would probably have the item cost on it too, rather than a constant.).
  - Configuration around minimum value on the cash card, rather than hardcoded.
  - Configuration around soft drink price, rather than hardcoded.


